using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float detectionDistance;
    public GameObject player;
    // public Gun[] weapons;
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
    public AudioSource detect;
    private bool playAudio;
    private GameObject parent;
    private List<GameObject> enemyList;
    private Vector3 myFacePosition;
    
    void Start() 
    {
        playAudio = true;
        // CheckActiveWeapons();
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
            myFacePosition = new Vector3(transform.position.x, transform.position.y + 3, transform.position.z);
            HandleSprintCrouching();
            this.CheckForTargetInLineOfSight();
            this.HandleEnemyAlerts();
        }
    }

    private void CheckForTargetInLineOfSight()
    {
        
        if (Physics.Linecast(myFacePosition, playerTransform.position, out hit))
        {
            Debug.DrawLine(myFacePosition, hit.point, Color.green);
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
                Debug.DrawLine(myFacePosition, hit.point,Color.red);
                PlayerMovement pm = hit.transform.GetComponent<PlayerMovement>();
                enemyState.isPlayerDetected = true;

                if (playAudio)
                {
                    StartCoroutine(PlaySound(detect));
                    playAudio = false;
                }
                if (!detect.isPlaying)
                {
                    StartCoroutine(Wait(10.0f));
                }
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
        Vector3 enemyFacePosition = new Vector3(
            enemyOther.transform.position.x, 
            enemyOther.transform.position.y+3,
            enemyOther.transform.position.z);

        if(Physics.Linecast(myFacePosition, enemyFacePosition, out hit))
        {
            Debug.DrawLine(myFacePosition, hit.point, Color.magenta);
            float distance = Vector3.Distance(enemyFacePosition, myFacePosition);
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
                Debug.DrawLine(myFacePosition, hit.point, Color.black);

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

    IEnumerator PlaySound(AudioSource audio)
    {
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
    }

    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playAudio = true;
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
    
        return list;
    }

    void CheckActiveWeapons()
    {
        //WeaponSwitch playerWeapons = GameObject.FindWithTag("WeaponHolder").GetComponent<WeaponSwitch>();

        //weapons[0] = playerWeapons.weapons[ playerWeapons.GetPrimaryWeapon() ].GetComponent<Gun>();
        //weapons[1] = playerWeapons.weapons[ playerWeapons.GetSecondaryWeapon() ].GetComponent<Gun>();
    }

}
