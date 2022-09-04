using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : Actor
{
    [SerializeField] GameObject playerTarget;
    GameObject actionTarget;
    [SerializeField] float fleeSpeedMultiplier = 3f;
    float fleeDistance = 1.0f;
    [SerializeField] Vector3 direction;
    [SerializeField] Vector3 movement;
    // target is used by minion for following
    Animator anim;

    private void Start()
    {
        healthPoints = 10;
        moveSpeed = .8f;
        anim = GetComponent<Animator>();
        Roam();
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(RandomDirection());

    }

    private void LateUpdate()
    {
        if (status != "catched" && status != "eating") // otherwise do nothing
        {
            Move();
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        InteractWith(collision.gameObject);
    }


    bool CloseToPlayer()
    {
        return (playerTarget && Vector3.Distance(playerTarget.transform.position, gameObject.transform.position) <= fleeDistance);
    }

    public override void Move()
    {
        float actualMoveSpeed = moveSpeed;
        if (status == "fleeing")
            actualMoveSpeed *= fleeSpeedMultiplier;

        transform.Rotate(direction);

        movement = Vector3.forward * actualMoveSpeed;
        transform.Translate(movement * Time.deltaTime);
    }

    public override void InteractWith(GameObject gameObject)
    {
        Debug.Log("Interacting with " + gameObject.name);
        actionTarget = gameObject;

        if (false && actionTarget.tag == "Player")
        {
            StartCoroutine(Flee());
        }
        else if (actionTarget.tag == "Food")
        {
            StartCoroutine(Eat());
        }

    }

    public void GotCatched()
    {
        status = "catched";
        anim.SetBool("Eat", false);
        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);

        // start animation
        anim.SetBool("Turn Head", true);
        transform.SetParent(playerTarget.gameObject.transform);
        transform.Translate(new Vector3(0, 2f, 0));
    }

    IEnumerator Flee()
    {
        status = "fleeing";
        //anim.SetBool("Idle", true);
        anim.SetBool("Run", true);

        // eat object stopping movement
        Debug.Log("Started fleeing");

        // start eating animation
        yield return new WaitForSeconds(5);

        Roam();

    }

    void Roam()
    {
        status = "roaming";
        anim.SetBool("Eat", false);
        anim.SetBool("Turn Head", false);
        anim.SetBool("Run", false);

        anim.SetBool("Walk", true);
    }

    IEnumerator RandomDirection()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);
            direction = new Vector3(0, Random.Range(.3f, 1.0f), 0);
        }
    }

    IEnumerator Eat()
    {
        // eat object stopping movement
        Debug.Log("Started eating");
        status = "eating";

        anim.SetBool("Walk", false);
        anim.SetBool("Turn Head", false);
        anim.SetBool("Run", false);

        anim.SetBool("Eat", true);

        // start eating animation
        yield return new WaitForSeconds(5);

        if (status != "fleeing")
            Roam();
    }


}
