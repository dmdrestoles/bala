using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float movementSpeed = 4f;
    public EnemyState enemyState;

    [HideInInspector]
    public Transform playerTransform;
    public bool isPlayerDetected;
    public Vector3 startPatrolLocation;
    public Vector3 endPatrolLocation;
    NavMeshAgent agent;
    Rigidbody r;
    private Vector3 playerLastPosition;
    private bool knowsLastPosition = false;
    private bool patrolStarted = false;

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
        HandleDetection(isPlayerDetected);
    }

    private void HandleDetection(bool isPlayerDetected){
        if (isPlayerDetected)
        {
            patrolStarted = false;
            agent.isStopped = false;
            agent.destination = playerTransform.position;
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            playerLastPosition = playerTransform.position;
            knowsLastPosition = true;
            r.velocity *= 0.99f;
        }
        if (knowsLastPosition)
        {
            agent.SetDestination(playerLastPosition);
        }
        if(CheckDestinationReached(playerLastPosition))
        {
            knowsLastPosition = false;
            StopMovement();
        }
        if(!isPlayerDetected && !knowsLastPosition)
        {
            ResumeMovement();
            Patrol(startPatrolLocation,endPatrolLocation);
        }

    }
    bool CheckDestinationReached(Vector3 target) {
        float distanceToTarget = Vector3.Distance(transform.position, target);
        bool result = false;
        if(distanceToTarget < 2)
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
        if ((CheckDestinationReached(start)) && !agent.pathPending )
        {
            agent.SetDestination(end);
            Debug.Log("end: "+agent.destination);
        }
        else if ((CheckDestinationReached(end)) && !agent.pathPending )
        {
            agent.SetDestination(start);
            Debug.Log("start: "+agent.destination);
        }
        else if(!patrolStarted){
            patrolStarted = true;
            agent.SetDestination(start);
            Debug.Log(agent.destination);
        }
    }

}
