using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float nextTimeToFire = 0f;
    public float fireRate = 1f;

    public string weaponName;

    public bool isActive = false;
    public bool isReliable = true;

    public Animator animator;
    public GameObject weapon;
    private RaycastHit hit;

    public float meleeRange;
    public float damage;
    private bool isHitting;

    public void CheckForEnemies()
    {
        isHitting = Physics.Linecast(transform.position, transform.position + (transform.forward * meleeRange), out hit);

        if (isHitting && hit.transform.CompareTag("Enemy") && IsEnemyWithinAngle(hit) && IsEnemyWithinHitDistance(hit))
        {
            Debug.Log(hit.transform.name);
            hit.transform.gameObject.GetComponent<EnemyState>().TakeDamage(damage);
        }
    }

    private bool IsEnemyWithinAngle(RaycastHit hit)
    {
        bool result = false;
        float deg = Vector3.Angle( transform.forward, hit.transform.position - transform.position );
        if(deg <= 45)
        {
            result = true;
        }
        return result;
    }

    private bool IsEnemyWithinHitDistance(RaycastHit hit)
    {
        bool result = false;
        if (hit.distance <= meleeRange)
        {
            result = true;
        }
        return result;
    }   
}
