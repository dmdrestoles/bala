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
    public bool isPlayerDetected;
    public Vector3 startPatrolLocation;
    public Vector3 endPatrolLocation;
    NavMeshAgent agent;
    Rigidbody r;
    private Vector3 playerLastPosition;
    private bool knowsLastPosition = false;
    private bool patrolStarted = false;
    private float timer = 0.0f;
    private Animator animator;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        r = GetComponent<Rigidbody>();
        isPlayerDetected = enemyState.isPlayerDetected;
    }

    void Update()
    {
        isPlayerDetected = enemyState.isPlayerDetected;
        if (!enemyState.isAsleep && animator.GetBool("isMoving"))
        {
            HandleDetection(isPlayerDetected);
        }
        else if(enemyState.isFiring)
        {
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
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
            agent.speed = 20f;
            animator.SetBool("isMoving", true);

            enemyState.distanceFromPlyaer = Vector3.Distance(transform.position, playerTransform.position);

            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            HoldStillToFire();
        }
        if (knowsLastPosition && !isPlayerDetected)
        {
            animator.SetBool("isMoving", true);
            agent.SetDestination(playerLastPosition);
        }
        if(CheckDestinationReached(playerLastPosition,2) && knowsLastPosition)
        {
            Vector3 start = RandomNavSphere(playerLastPosition, 3.5f, -1); 
            Vector3 end = RandomNavSphere(playerLastPosition, 3.5f, -1);
            
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

    public void StopMovement(){
        r.freezeRotation = true;
        r.constraints = RigidbodyConstraints.FreezePosition;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
        animator.SetBool("isMoving", false);

    }

    public void ResumeMovement() {
        r.freezeRotation = false;
        r.constraints = RigidbodyConstraints.None;
        agent.isStopped = false;
        animator.SetBool("isMoving", true);
    }

    void Patrol(Vector3 start, Vector3 end){
        animator.SetBool("isMoving", true);
        animator.SetBool("isWalking", true);
        agent.speed = 3.0f;
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
        agent.speed = 3;
        animator.SetBool("isMoving", true);
        animator.SetBool("isWalking", true);

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
            timer = timer - time;
            knowsLastPosition = false;
            enemyState.alertLevel = 0;
            agent.acceleration = 4;
            StopMovement();
        }
    }

    void HoldStillToFire(){
        if (CheckDestinationReached(playerTransform.position,20))
        {
            StopMovement();
            enemyState.isFiring = true;
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
