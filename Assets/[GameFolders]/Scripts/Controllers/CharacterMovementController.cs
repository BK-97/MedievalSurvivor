using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    private Rigidbody rb = null;
    public float moveSpeed=3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move()
    {
        Vector3 moveDirection = InputManager.Instance.GetDirection();
        rb.velocity = moveDirection * moveSpeed;
    }
   
}
