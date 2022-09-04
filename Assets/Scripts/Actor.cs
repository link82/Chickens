using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Actor : MonoBehaviour
{
    public int healthPoints { get; protected set; }
    [SerializeField] protected string status = "idle";
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float moveSpeedCoefficient;
    [SerializeField] protected float rotationSpeed = 100.0f;
    protected Rigidbody rb;
    public GameObject target;
    // todo: animator

    public abstract void InteractWith(GameObject gameObject);
    public abstract void Move();

}
