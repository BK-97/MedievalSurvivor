using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovementController : MonoBehaviour
{
    private Rigidbody rb = null;
    private float maxSpeed;
    private float currentSpeed;
    private float acceleration=4;
    const float ROTATE_SPEED=720;
    const float ACCELERATION = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetSpeed(int speed)
    {
        maxSpeed = speed;
    }
    public void Move()
    {
        Vector3 moveDirection = InputManager.Instance.GetDirection();
        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * ACCELERATION);
        rb.velocity = moveDirection * currentSpeed;
    }
    public void MoveEnd()
    {
        rb.velocity = Vector3.zero;
        float duration = 0.1f;
        float targetSpeed = 0f;
        
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(DOTween.To(() => currentSpeed, x => currentSpeed = x, targetSpeed, duration)).SetEase(Ease.Linear);
    }
    public void RotateTowards()
    {
        Vector3 rotateDirection = InputManager.Instance.GetDirection();
        if (rotateDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotateDirection,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation,Time.deltaTime*ROTATE_SPEED);
        }
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
}
