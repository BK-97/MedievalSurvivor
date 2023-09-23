using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
        NavMeshAgent.SetDestination(transform.position);
    }
    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
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
