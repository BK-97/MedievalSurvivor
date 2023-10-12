using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class CharacterMovementController : MonoBehaviour
{
    #region Params
    public RectTransform Indicator;

    public bool canRollOver;

    private Rigidbody rb = null;

    private float maxSpeed;
    private float currentSpeed;
    private float rollOverCoolDown =3;

    const float ROTATE_SPEED=720;
    const float ACCELERATION = 3;

    private CharacterAnimationController animController;
    public CharacterAnimationController AnimController { get { return (animController == null) ? animController = GetComponent<CharacterAnimationController>() : animController; } }
    #endregion
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    #region MoveMethods
    public void Move()
    {
        if (AnimController.IsRolling())
            return;
        Vector3 moveDirection = InputManager.Instance.GetDirection();
        currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * ACCELERATION);
        rb.velocity = moveDirection * currentSpeed;
        AnimController.SetSpeed(currentSpeed, maxSpeed);
    }
    public void MoveEnd()
    {
        rb.velocity = Vector3.zero;
        float duration = 0.1f;
        float targetSpeed = 0f;
        
        Sequence sequence = DOTween.Sequence();
        
        sequence.Append(DOTween.To(() => currentSpeed, x => currentSpeed = x, targetSpeed, duration)).SetEase(Ease.Linear)
            .OnUpdate(()=> AnimController.SetSpeed(currentSpeed, maxSpeed)).OnComplete(()=> AnimController.SetSpeed(0, maxSpeed));
    }
    private void Update()
    {
        Vector3 rotateDirection = InputManager.Instance.GetMouseWorldPos() - transform.position;
        Quaternion toRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
        Indicator.rotation = Quaternion.RotateTowards(Indicator.rotation, toRotation, Time.deltaTime * ROTATE_SPEED);
    }
    public void RotateTowards()
    {
        Vector3 rotateDirection = InputManager.Instance.GetDirection();
        if (rotateDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(rotateDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, Time.deltaTime * ROTATE_SPEED);
        }
    }
    public void RollOver()
    {
        canRollOver = false;
        StartCoroutine(WaitRollOverCO());
    }
    IEnumerator WaitRollOverCO()
    {
        yield return new WaitForSeconds(rollOverCoolDown);
        canRollOver = true;
    }
    #endregion
    #region GetSet
    public void SetSpeed(int speed)
    {
        maxSpeed = speed;
        canRollOver = true;
    }
    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }
    #endregion

}
