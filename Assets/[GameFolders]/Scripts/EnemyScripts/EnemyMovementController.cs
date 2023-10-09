using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class EnemyMovementController : MonoBehaviour
{
    private float maxSpeed;
    
    private Transform targetTransform;
    private Vector3 targetPos;
    bool canMove=false;
    private NavMeshAgent navMeshAgent;
    public NavMeshAgent NavMeshAgent { get { return (navMeshAgent == null) ? navMeshAgent = GetComponent<NavMeshAgent>() : navMeshAgent; } }
    public void SetSpeed(float speed)
    {
        maxSpeed = speed;
        NavMeshAgent.speed = maxSpeed;
        canMove = true;

    }
    public void Move()
    {
        if (!canMove)
            return;
        if (NavMeshAgent == null)
            return;
        if (!NavMeshAgent.enabled)
            return;
        if (!gameObject.activeSelf)
            return;
        if (isBackStepping)
            return;
        if (targetTransform == null)
        {
            NavMeshAgent.SetDestination(transform.position);
            return;
        }
        if (IsDestinationReached())
        {
            targetPos = transform.position;
        }
        else
            targetPos = targetTransform.position;
        NavMeshAgent.SetDestination(targetPos);

    }
    public void MoveEnd()
    {
        if(NavMeshAgent.enabled)
            NavMeshAgent.SetDestination(transform.position);
    }
    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }
    bool isBackStepping;
    public void BackStep()
    {
        if (isBackStepping)
            return;
        isBackStepping = true;
        
        //NavMeshAgent.enabled = false;
        Vector3 backStepPos = transform.position - transform.forward * 0.5f;
        transform.DOMove(backStepPos, 0.1f).OnComplete(BackStepEnd);
    }
    void BackStepEnd()
    {
        NavMeshAgent.enabled = true;
        isBackStepping = false;
    }
    private bool IsDestinationReached()
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);

        if (NavMeshAgent.stoppingDistance >= distance)
            return true;
        else
            return false;
    }
    public float GetCurrentSpeed()
    {
        return NavMeshAgent.velocity.magnitude;
    }
}
