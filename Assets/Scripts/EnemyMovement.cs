using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 10f;
    public EnemyState enemyState;
    public Transform playerTransform;

    [HideInInspector]
    //public Transform playerTransform;
    public bool isPlayerDetected;
    public Vector3 startPatrolLocation;
    public Vector3 endPatrolLocation;
    NavMeshAgent agent;
    Rigidbody r;
    private Vector3 playerLastPosition;
    private bool knowsLastPosition = false;
    private bool patrolStarted = false;
    private float timer = 0.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        isPlayerDetected = enemyState.isPlayerDetected;
        StopMovement();
    }

    void Update()
    {
        isPlayerDetected = enemyState.isPlayerDetected;
        if (!enemyState.isAsleep)
        {
            HandleDetection(isPlayerDetected);
        }
        else
        {
            StopMovement();
        }
    }

    private void HandleDetection(bool isPlayerDetected){
        if (isPlayerDetected)
        {
            patrolStarted = false;
            agent.isStopped = false;
            knowsLastPosition = true;

            agent.destination = playerTransform.position;
            playerLastPosition = playerTransform.position;
            r.velocity *= 0.99f;

            enemyState.distanceFromPlyaer = Vector3.Distance(transform.position, playerTransform.position);

            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            HoldStillToFire();
        }
        if (knowsLastPosition)
        {
            agent.SetDestination(playerLastPosition);
        }
        if(CheckDestinationReached(playerLastPosition,2) && knowsLastPosition)
        {
            Vector3 start = RandomNavSphere(playerLastPosition, 4, -1); 
            Vector3 end = RandomNavSphere(playerLastPosition, 4, -1);
            PatrolKnownLastPosition(start, end,2);
            
        }
        if(!isPlayerDetected && !knowsLastPosition)
        {
            ResumeMovement();
            Patrol(startPatrolLocation,endPatrolLocation);
        }

    }
    bool CheckDestinationReached(Vector3 target, float stopDistance) {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        bool result = false;
        if(distanceToTarget < stopDistance)
        {
            result = true;
        }
        return result;
    }

    void StopMovement(){
        r.freezeRotation = true;
        r.constraints = RigidbodyConstraints.FreezePosition;
        agent.isStopped = true;
    }

    void ResumeMovement() {
        r.freezeRotation = false;
        r.constraints = RigidbodyConstraints.None;
        agent.isStopped = false;
    }

    void Patrol(Vector3 start, Vector3 end){
        
        if ((CheckDestinationReached(start,1)) && !agent.pathPending )
        {
            agent.SetDestination(end);
        }
        else if ((CheckDestinationReached(end,1)) && !agent.pathPending )
        {
            agent.SetDestination(start);
        }
        else if(!patrolStarted)
        {
            patrolStarted = true;
            agent.SetDestination(start);
        }
    }

    void PatrolKnownLastPosition(Vector3 start, Vector3 end, float time){
        agent.acceleration = 3;
        if ((CheckDestinationReached(start,1)) && !agent.pathPending )
        {
            agent.SetDestination(end);
        }
        else if ((CheckDestinationReached(end,1)) && !agent.pathPending )
        {
            agent.SetDestination(start);
        }

        timer += Time.deltaTime;

        if (timer > time)
        {
            // Remove the recorded seconds.
            timer = timer - time;
            knowsLastPosition = false;
            enemyState.alertLevel = 0;
            agent.acceleration = 8;
            StopMovement();
            
        }
    }

    void HoldStillToFire(){
        if (CheckDestinationReached(playerTransform.position,15))
        {
            StopMovement();
        }
    }

    private Vector3 RandomNavSphere (Vector3 origin, float distance, int layermask) {
        Vector3 result = Vector3.zero;
        NavMeshHit navHit;

        NavMesh.SamplePosition (origin, out navHit, distance, NavMesh.AllAreas);
        result = navHit.position;
        return result;
    }
}
