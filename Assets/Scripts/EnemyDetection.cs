﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float detectionDistance;
    public GameObject player;
    public Gun[] weapons;
    private RaycastHit hit;
    public PlayerState playerState;
    public Detection_Utils utils;
    public Gun primary, secondary;
    [HideInInspector]
    private Transform playerTransform;
    private Animator animator;
    private EnemyState enemyState; 
    private EnemyMovement enemyMovement;
    private float originalDetectionDistance;
    private GameObject parent;
    private List<GameObject> enemyList;
    
    void Start() 
    {
        CheckActiveWeapons();
        originalDetectionDistance = detectionDistance;
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
        enemyMovement = GetComponent<EnemyMovement>();
        parent = transform.parent.gameObject;
        enemyList = GetListEnemies(parent);
    }
    void Update() 
    {
        playerTransform = player.transform;
        if (!enemyState.isAsleep)
        {
            HandleSprintCrouching();
            this.CheckForTargetInLineOfSight();
            this.HandleEnemyAlerts();
        }
    }

    private void CheckForTargetInLineOfSight()
    {
        if (Physics.Linecast(transform.position, playerTransform.position, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green);
            if( (primary.isFiring && !primary.isSilent) || (secondary.isFiring && !secondary.isSilent) )
            {
                animator.SetBool("isWalking", false);
                enemyState.alertLevel = 1;
                enemyState.isPlayerDetected = true;
                StartCoroutine(HandleGunFiring());
            }
            else if (hit.transform.CompareTag("Player") && utils.IsHitWithinObjectAngle(hit, transform, 45)
                    && utils.IsHitWithinObjectDistance(hit, detectionDistance) && IsPlayerVisible())
            {
                animator.SetBool("isWalking", false);
                enemyState.alertLevel = 1;
                Debug.DrawLine(transform.position, hit.point,Color.red);
                PlayerMovement pm = hit.transform.GetComponent<PlayerMovement>();
                enemyState.isPlayerDetected = true;
            }
            else if(enemyState.hasPatrol)
            {
                enemyState.isPlayerDetected = false;
                enemyState.isFiring = false;
                enemyMovement.ResumeMovement();
            }
            /* commented out for bug fixing later on
            else 
            {
                enemyState.isPlayerDetected = false;
                enemyState.isFiring = false;
            }*/
        }
        else
        {
            enemyState.isFiring = false;
        }
        
    }

    IEnumerator ConnectEnemyLineCast(GameObject enemyOther) 
    {
        if(Physics.Linecast(transform.position, enemyOther.transform.position, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.magenta);
            Debug.Log(transform.position + "----" +enemyOther.transform.position);
            float distance = Vector3.Distance(enemyOther.transform.position, transform.position);
            EnemyState enemyOtherState = enemyOther.GetComponent<EnemyState>();

            if (enemyState.isPlayerDetected && distance < 10)
            {
                Debug.DrawLine(transform.position, hit.point, Color.black);

                yield return new WaitForSeconds(2);
                enemyOtherState.isPlayerDetected = true;
                enemyOtherState.hasPatrol = false;
            }
            else if (enemyState.isFiring)
            {
                Debug.DrawLine(transform.position, hit.point, Color.black);

                yield return new WaitForSeconds(3);
                enemyOtherState.isPlayerDetected = true;
                enemyOtherState.hasPatrol = false;
            }
        }
    }

    private void HandleEnemyAlerts()
    {
        foreach (var enemy in enemyList)
        {
            if (transform.name == enemy.name)
            {
                continue;
            } 
            else 
            {
                StartCoroutine(ConnectEnemyLineCast(enemy));
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
        primary.isFiring = false;
        secondary.isFiring = false;
    }

    void HandleSprintCrouching()
    {
        if (playerState.isCrouching)
        {
            detectionDistance = originalDetectionDistance * 0.5f;
        } else if (playerState.isSprinting)
        {
            detectionDistance = originalDetectionDistance * 2.0f;
        } else
        {
            detectionDistance = originalDetectionDistance;
        }
    }

    public List<GameObject> GetListEnemies(GameObject Go)
    {
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i< Go.transform.childCount; i++)
        {
            list.Add(Go.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i< list.Count; i++)
        {
            Debug.Log(list[i].name);
        }
    
        return list;
    }

    void CheckActiveWeapons()
    {
        //WeaponSwitch playerWeapons = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponSwitch>();

        //weapons[0] = playerWeapons.weapons[ playerWeapons.GetPrimaryWeapon() ].GetComponent<Gun>();
        //weapons[1] = playerWeapons.weapons[ playerWeapons.GetSecondaryWeapon() ].GetComponent<Gun>();
    }

}
