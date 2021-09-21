﻿using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;
    public float reloadTime = 3f;
    public float bulletReload = 0.5f;

    public int maxAmmo = 50;
    public int magazineAmmo = 5;
    public int currentAmmo;

    public bool isReliable = true;

    public AudioSource fireSound;
    public AudioSource fullReloadSound;
    public AudioSource startReloadSound;
    public AudioSource loadBulletSound;
    public AudioSource endReloadSound;
    public Animator animator;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash;
    // public ParticleSystem impactEffect;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = magazineAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }
    void Update()
    {
        if (isReloading)
        {
            return;
        }

        if (currentAmmo <= 0 && maxAmmo <= 0)
        {
            // play blank fire sound
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineAmmo && maxAmmo > 0)
        {
            StartCoroutine(HotReload());
            return;
        }

        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            if (currentAmmo > 0)
            {
                Shoot();

                if (!isReliable)
                {
                    Destroy(this.gameObject, 1.5f);
                }
            }
            else
            {
                StartCoroutine(HotReload());
            }
        }

    }

    /*IEnumerator FullReload()
    {
        yield return new WaitForSeconds(1f);
        isReloading = true;
        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime - 0.25f);

        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        currentAmmo = magazineAmmo;
        isReloading = false;

    }*/

    IEnumerator HotReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Reloading", true);
        startReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        while (currentAmmo < magazineAmmo)
        {
            loadBulletSound.Play();
            maxAmmo -= 1;
            currentAmmo += 1;
            Debug.Log("Loading ammo: " + currentAmmo);
            yield return new WaitForSeconds(bulletReload);
        }

        endReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Reloading", false);
        yield return new WaitForSeconds(0.25f);
        isReloading = false;

    }
    void Shoot()
    {
        fireSound.Play();
        currentAmmo -= 1;
        muzzleFlash.Play();
        RaycastHit hit;
        Debug.DrawRay(fpsCamera.transform.position, fpsCamera.transform.forward * range, new Color(255, 0, 0), 2f);
        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            //Target target = hit.transform.GetComponent<Target>();
            EnemyState enemy = hit.transform.GetComponent<EnemyState>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            /*if (target != null)
            {
                target.TakeDamage(damage);
            }*/


            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            /* GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
             * Destroy(impact, 0.5f);
             */
        }
    }
}
