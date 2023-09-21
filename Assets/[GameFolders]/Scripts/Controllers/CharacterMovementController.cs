using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    private Rigidbody rb = null;
    public float moveSpeed=3;
    public float rotationSpeed = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move()
    {
        Vector3 moveDirection = InputManager.Instance.GetDirection();
        rb.velocity = moveDirection * moveSpeed;
    }
    private void Update()
    {
        if (!LevelManager.Instance.IsLevelStarted)
            return;
        Vector3 direction =InputManager.Instance.GetMouseWorldPos() - transform.position ;
        direction.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
