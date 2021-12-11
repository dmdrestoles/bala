using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public EnemyState enemyState;
    public Transform enemyTransform;
    public Transform playerTransform;
    public bool isSteady;

    [HideInInspector]
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
        if (isSteady == false)
        {
            CutSceneMovement(endPatrolLocation);
        }
        else
        {
            StopMovement();
        }
        isPlayerDetected = enemyState.isPlayerDetected;
        HandleDetection(isPlayerDetected);
    }

    private void CutSceneMovement(Vector3 end)
    {
        agent.SetDestination(end);
        if (CheckDestinationReached(end))
        {
            var lookPos = playerTransform.position - enemyTransform.position;
            lookPos.y = 2;
            var rotation = Quaternion.LookRotation(lookPos);
            enemyTransform.rotation = Quaternion.Slerp(enemyTransform.rotation, rotation, Time.deltaTime * 2);

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
            //enemyTransform.rotation.y = playerTransform.rotation.y + 180;
            result = true;
        }
        return result;
    }

    void StopMovement()
    {
        r.constraints = RigidbodyConstraints.FreezePosition;
        r.constraints = RigidbodyConstraints.FreezeRotation;
        agent.isStopped = true;
    }
}

