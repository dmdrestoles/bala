﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float detectionDistance;
    public GameObject player;
    public Gun[] weapons;
    private RaycastHit hit;
    public EnemyState enemyState; 
    public PlayerState playerState;
    public Detection_Utils utils;
    private Transform playerTransform;

    
    void Start() 
    {
        CheckActiveWeapons();
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
            Debug.DrawLine(transform.position, hit.point, Color.green);
            if( (weapons[0].isFiring && !weapons[0].isSilent) || (weapons[1].isFiring && !weapons[1].isSilent) )
            {
                enemyState.alertLevel = 1;
                enemyState.isPlayerDetected = true;
                StartCoroutine(HandleGunFiring());
            }
            else if (hit.transform.CompareTag("Player") && utils.IsHitWithinObjectAngle(hit, transform, 45)
                    && utils.IsHitWithinObjectDistance(hit, detectionDistance) && IsPlayerVisible())
            {
                enemyState.alertLevel = 1;
                Debug.DrawLine(transform.position, hit.point,Color.red);
                PlayerMovement pm = hit.transform.GetComponent<PlayerMovement>();
                enemyState.isPlayerDetected = true;
            }
            else
            {
                enemyState.isPlayerDetected = false;
            }
        }
        
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
        yield return new WaitForSeconds (1);
        weapons[0].isFiring = false;
        weapons[1].isFiring = false;
    }

    void CheckActiveWeapons()
    {
        WeaponSwitch playerWeapons = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponSwitch>();

        weapons[0] = playerWeapons.weapons[ playerWeapons.GetPrimaryWeapon() ].GetComponent<Gun>();
        weapons[1] = playerWeapons.weapons[ playerWeapons.GetSecondaryWeapon() ].GetComponent<Gun>();
    }

}
