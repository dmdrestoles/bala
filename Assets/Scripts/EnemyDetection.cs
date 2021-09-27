﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float mTargetDetectionDistance;
    private bool isDetecting;

    public GameObject player;
    public Gun rifle, revolver, paltik;
    private RaycastHit hit;
    public EnemyState enemyState; 
    private Transform playerTransform;

    public Material materialOnAlert;
    public Material materialOnNormal;
    void Start() 
    {
    }
    void Update() 
    {
        playerTransform = player.transform;
        this.CheckForTargetInLineOfSight();
    }

    private void CheckForTargetInLineOfSight()
    {
        isDetecting = Physics.Linecast(transform.position, playerTransform.position, out hit);
        MeshRenderer mr = GetComponent<MeshRenderer>();
        if(rifle.isFiring || revolver.isFiring || paltik.isFiring)
        {
            enemyState.isPlayerDetected = true;
            StartCoroutine(HandleGunFiring());
        }
        else if (isDetecting && hit.transform.CompareTag("Player") && IsPlayerWithinFOV() && IsPlayerWithinSeeDistance(hit))
        {
            Debug.DrawLine(transform.position, hit.point,Color.red);
            PlayerMovement pm = hit.transform.GetComponent<PlayerMovement>();
            enemyState.isPlayerDetected = pm.checkVisibility();
            mr.material = materialOnAlert;
        }
        else
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            enemyState.isPlayerDetected = false;
            mr.material = materialOnNormal;
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
        if (hit.distance <= mTargetDetectionDistance)
        {
            result = true;
        }
        return result;
    }

    IEnumerator HandleGunFiring()
    {
        yield return new WaitForSeconds (1);
        rifle.isFiring = false;
        revolver.isFiring = false;
        paltik.isFiring = false;
    }

}
