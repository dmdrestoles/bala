using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public static float nextTimeToMelee = 0f;
    public static float attackSpeed = 0.9f;

    public string weaponName;

    public bool isActive = false;
    public bool isReliable = true;
    public static bool isBoloAcquired = false;

    public Animator animator;
    public GameObject weapon;
    public Transform playerTransform;
    public Detection_Utils utils;
    public float meleeRange;
    public float meleeAngle;
    public float damage;
    private bool isHitting;
    private RaycastHit hit;

    void Start()
    {
        meleeAngle = 90;
    }

    void Update()
    {

    }

    public void CheckForEnemies()
    {
        //Debug.Log("Melee attack!");
        isHitting = Physics.Linecast(playerTransform.position, transform.position + (transform.forward * meleeRange), out hit);
        if (isHitting)
        {
            //Debug.Log("something was hit!");
            Debug.Log(hit.transform.name);
            Debug.Log(hit.distance);
            float deg = Vector3.Angle( playerTransform.forward, hit.transform.position - playerTransform.position );
            Debug.Log(deg);
            
        }
        if (isHitting && hit.transform.CompareTag("Enemy") && utils.IsHitWithinObjectAngle(hit, playerTransform, meleeAngle) 
            && utils.IsHitWithinObjectDistance(hit,meleeRange))
        {
            Debug.Log("Enemy hit!");
            hit.transform.GetComponent<GruntStateManager>().health -= damage;
        }
    }
 
}
