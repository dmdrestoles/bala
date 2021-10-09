using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public EnemyState enemyState;

    [HideInInspector]
    public Transform playerTransform;
    public bool isPlayerDetected;
    public Vector3 endPatrolLocation;
    NavMeshAgent agent;
    Rigidbody r;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        isPlayerDetected = enemyState.isPlayerDetected;
        CutSceneMovement(endPatrolLocation);
    }

    void Update()
    {
        CutSceneMovement(endPatrolLocation);
        isPlayerDetected = enemyState.isPlayerDetected;
        HandleDetection(isPlayerDetected);
    }

    private void CutSceneMovement(Vector3 end)
    {
        agent.SetDestination(end);
        if (CheckDestinationReached(end))
        {
            StopMovement();
        }

    }

    private void HandleDetection(bool isPlayerDetected)
    {
        if (isPlayerDetected)
        {
            // Insert Enemy Shooting Mechanic
        }
    }

    bool CheckDestinationReached(Vector3 target)
    {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        bool result = false;
        if (distanceToTarget < 0.5f)
        {
            result = true;
        }
        return result;
    }

    void StopMovement()
    {
        r.freezeRotation = true;
        r.constraints = RigidbodyConstraints.FreezePosition;
        agent.isStopped = true;
    }
}

