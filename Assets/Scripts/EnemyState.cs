using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    public bool isPlayerDetected = false;
    public bool isAsleep = false;
    public bool hasPatrol = true;
    public float health = 50f;
    public float distanceFromPlyaer;
    [HideInInspector]
    public bool isAiming, isWalking, isFiring, isDead;
    public int ammo, alertLevel;
    private float waitTime = 10.0f;
    private float timer = 0.0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isAsleep && !isDead)
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
            isDead = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        animator.SetTrigger("triggerDeath");
        yield return new WaitForSeconds(1.0f);
        isAsleep = true;
        yield return new WaitForSeconds(1000000.0f);
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
