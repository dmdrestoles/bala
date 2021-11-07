using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    public bool isPlayerDetected = false;
    public bool isAsleep = false;
    public float health = 50f;
    public float distanceFromPlyaer;
    [HideInInspector]
    public bool isAiming, isWalking, isFiring;
    public int ammo, alertLevel;
    private float waitTime = 10.0f;
    private float timer = 0.0f;

    void Update()
    {
        if (isAsleep)
        {
            // mr.material = materialOnSleep;
            HandleSleeping();
        }
        
        else if (isPlayerDetected)
        {
            // mr.material = materialOnAlert; 
        }

        else if (!isPlayerDetected && !isAsleep)
        {
            // mr.material = materialOnNormal;
        }
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    private void HandleSleeping() 
    {
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            // Remove the recorded 2 seconds.
            timer = timer - waitTime;
            isAsleep = false;
            
        }
    }
}
