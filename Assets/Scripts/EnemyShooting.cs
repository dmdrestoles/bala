﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public EnemyState state;
    public GameObject gun, player, bullet;
    public AudioSource fireSound, fullReloadSound, startReloadSound;
    public AudioSource loadBulletSound, endReloadSound;
    public ParticleSystem muzzleFlash;
    public int magazineAmmo = 5;
    public int currentAmmo = 5;
    public float bulletReload = 0.5f;
    [HideInInspector]
    private float waitTime = 4.0f;
    private float timer = 0.0f;
    private Transform target;
    private bool isReloading;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = player.transform;
        if (!state.isAsleep)
        {
            HandleShooting();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        GameObject bulletForward;
        
        StartCoroutine(ShootAnimation());

        bool shootCast = Physics.Linecast(gun.transform.position, CalculateMiss(target.position), out hit);
        bulletForward = Instantiate(bullet, gun.transform.position, gun.transform.rotation);
        bulletForward.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 500);
        
        if (shootCast && hit.transform.tag == "Player")
        {
            Debug.DrawLine(transform.position, hit.point, Color.black, 2f);
            PlayerState player = hit.transform.GetComponent<PlayerState>();
            player.TakeDamage(25);
        }
    }

    void HandleShooting()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (state.isPlayerDetected)
        {
            timer += Time.deltaTime;
        }

        if (timer > waitTime && state.isPlayerDetected )
        {
            // Remove the recorded 2 seconds.
            timer = timer - waitTime;   
            Shoot();
            currentAmmo-=1;
        }
    }

    Vector3 CalculateMiss(Vector3 target){
        double missFactor = System.Math.Pow(2, 0.05*(double)state.distanceFromPlyaer) - 1;
        float a = (float)missFactor;

        float xRand = Random.Range(-a, a);
        float yRand = Random.Range(-a, a);
        float zRand = Random.Range(-a, a);

        target.x += xRand;
        target.y += yRand;
        target.z += zRand;

        return target;
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        startReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        while (currentAmmo < magazineAmmo)
        {
            loadBulletSound.Play();
            currentAmmo += 1;
            yield return new WaitForSeconds(bulletReload);
        }

        endReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds(0.25f);
        isReloading = false;

    }

    IEnumerator ShootAnimation()
    {
        animator.SetTrigger("triggerFire");
        Debug.Log("shooting");
        fireSound.Play();
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.15f);
        animator.ResetTrigger("triggerFire");
    }
}
