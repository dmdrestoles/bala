using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {
    public float mTargetDetectionDistance;
    private bool isDetecting;

    public GameObject player;
    public Gun[] weapons;
    private RaycastHit hit;
    public EnemyState enemyState; 
    private Transform playerTransform;

    public Material materialOnAlert;
    public Material materialOnNormal;
    void Start() 
    {
        CheckActiveWeapons();
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
    
        if(weapons[0].isFiring || weapons[1].isFiring)
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
        if(deg <= 45)
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
