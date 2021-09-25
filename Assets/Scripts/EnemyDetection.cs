﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float mRaycastRadius;
    public float mTargetDetectionDistance;

    private RaycastHit _mHitInfo;
    public EnemyState enemyState; 
    private bool isDetecting;
    void Start() 
    {
    }
    public void CheckForTargetInLineOfSight()
    {
        isDetecting = Physics.SphereCast(transform.position, mRaycastRadius, transform.forward, out _mHitInfo, mTargetDetectionDistance);
        if (isDetecting && _mHitInfo.transform.CompareTag("Player"))
        {
            Debug.Log("Player in vicinity");
            PlayerMovement pm = _mHitInfo.transform.gameObject.GetComponent<PlayerMovement>();
            enemyState.isPlayerDetected = pm.checkVisibility();
        } else
        {
            enemyState.isPlayerDetected = false;
        }
    }
    private void OnDrawGizmos()
    {
        if (isDetecting && _mHitInfo.transform.CompareTag("Player"))
        {
            Gizmos.color = Color.red;
        } else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawCube(new Vector3(0f, 0f, mTargetDetectionDistance / 2f), new Vector3(mRaycastRadius, mRaycastRadius, mTargetDetectionDistance));
    }
    void Update() 
    {
        this.CheckForTargetInLineOfSight();
    }
}
