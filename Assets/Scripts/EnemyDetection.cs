using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float mRaycastRadius;
    public float detectionDistance;
    private bool isDetecting;

    public GameObject player;
    public Gun rifle, revolver, paltik;
    private RaycastHit hit;
    public EnemyState enemyState; 
    public PlayerState playerState;
    private Transform playerTransform;

    
    void Start() 
    {
    }
    void Update() 
    {
        playerTransform = player.transform;
        if (!enemyState.isAsleep)
        {
            this.CheckForTargetInLineOfSight();
        }
    }

    private void CheckForTargetInLineOfSight()
    {
        if (Physics.Linecast(transform.position, playerTransform.position, out hit))
        {
            if(rifle.isFiring || revolver.isFiring || paltik.isFiring)
            {
                enemyState.alertLevel = 1;
                enemyState.isPlayerDetected = true;
                StartCoroutine(HandleGunFiring());
            }
            else if (hit.transform.CompareTag("Player") && IsPlayerWithinFOV() && IsPlayerWithinSeeDistance(hit) && IsPlayerVisible())
            {
                enemyState.alertLevel = 1;
                Debug.DrawLine(transform.position, hit.point,Color.red);
                PlayerMovement pm = hit.transform.GetComponent<PlayerMovement>();
                enemyState.isPlayerDetected = true;
            }
            else
            {
                Debug.DrawLine(transform.position, hit.point, Color.green);
                enemyState.isPlayerDetected = false;
            }
        }
        
    }

    private bool IsPlayerWithinFOV()
    {
        bool result = false;
        float deg = Vector3.Angle( transform.forward, playerTransform.position - transform.position );
        if( deg <= 45)
        {
            result = true;
        }
        return result;
    }

    private bool IsPlayerWithinSeeDistance(RaycastHit hit)
    {
        bool result = false;
        if (hit.distance <= detectionDistance)
        {
            result = true;
        }
        return result;
    }

    private bool IsPlayerVisible()
    {
        bool result = playerState.isVisible;
        if (enemyState.alertLevel == 1)
        {
            result = true;
        }
        return result;
    }

    IEnumerator HandleGunFiring()
    {
        yield return new WaitForSeconds (1.0f);
        rifle.isFiring = false;
        revolver.isFiring = false;
        paltik.isFiring = false;
    }

}
