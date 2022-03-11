using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialMovement : MonoBehaviour
{
    public float movementSpeed = 10f;

    public Transform playerTransform;
    public bool isSteady;
    public bool isFriendly;
    public AudioSource fireSound;
    public ParticleSystem muzzleFlash;

    [HideInInspector]
    public bool isPlayerDetected;
    public Vector3 endPatrolLocation;
    NavMeshAgent agent;
    Rigidbody r;
    private Animator animator;
    private EnemyState enemyState;
    private float timer = 0.0f;
    private float waitTime = 5.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        agent.speed = movementSpeed;
        r = GetComponent<Rigidbody>();
        isPlayerDetected = enemyState.isPlayerDetected;
        waitTime = Random.Range(3.0f, 6.0f);
        //CutSceneMovement(endPatrolLocation);
    }

    void Update()
    {
        if (isSteady == false && !enemyState.isAsleep)
        {
            CutSceneMovement(endPatrolLocation);
        } else if(isSteady && !enemyState.isAsleep)
        {
            enemyState.isAiming = true;
            animator.SetBool("isAiming", true);
            timer += Time.deltaTime;
        } 
        
        if (timer > waitTime && !enemyState.isAsleep)
        {
            // Remove the recorded 5 seconds.
            timer = timer - waitTime;   
            StartCoroutine(ShootAnimation());
        }
        HandleDetection(isPlayerDetected);
    }

    private void CutSceneMovement(Vector3 end)
    {
        animator.SetBool("isMoving", true);
        agent.SetDestination(end);
        if (CheckDestinationReached(end))
        {
            var lookPos = playerTransform.position - transform.position;
            lookPos.y = 2;
            var rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            StopMovement();
            isPlayerDetected = true;
            Debug.Log(agent.name + " stopped moving");
        }
        
    }

    private void HandleDetection(bool isPlayerDetected)
    {
        if (isPlayerDetected && isFriendly == false)
        {
            transform.LookAt(new Vector3(playerTransform.transform.position.x, transform.position.y, playerTransform.position.z));
            HoldStillToFire();
        }
        else
        {
            HoldStillToFire();
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
        animator.SetBool("isMoving", false);
        r.constraints = RigidbodyConstraints.FreezePosition;
        r.constraints = RigidbodyConstraints.FreezeRotation;
        agent.isStopped = true;
        isSteady = true;
    }

    void HoldStillToFire()
    {
        if (CheckDestinationReached(endPatrolLocation) || isSteady == true)
        {
            StopMovement();
            animator.SetBool("isAiming", true);
            enemyState.isFiring = true;
            enemyState.isPlayerDetected = true;
        }
    }

    IEnumerator ShootAnimation()
    {
        yield return new WaitForSeconds(1.0f);
        animator.SetTrigger("triggerFire");
        fireSound.Play();
        muzzleFlash.Play();
        yield return new WaitForSeconds(1.5f);
        animator.ResetTrigger("triggerFire");
    }
}

