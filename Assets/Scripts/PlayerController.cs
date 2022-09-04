using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float playerSpeed = 5f;
    float rotationSpeed = 150f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Move();   
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided!");
        if (collision.gameObject.tag == "chicken")
            Grab(collision.gameObject);
    }


    void Move()
    {
        Vector3 rotation = new Vector3(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);
        transform.Rotate(rotation * Time.deltaTime);
        Vector3 direction = Vector3.forward * playerSpeed * Input.GetAxis("Vertical");
        transform.Translate(direction * Time.deltaTime);
    }

    void Grab(GameObject chickenObject)
    {
        Animal chicken = chickenObject.GetComponent<Animal>();
        chicken.GotCatched();
        //Destroy(chickenObject);
    }
}
