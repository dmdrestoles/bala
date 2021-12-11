﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    
    public string weaponName;
    public Image weaponImage;

    public bool isReliable = true;
    public bool isActive = false;
    public bool isEnemy = false;
    public bool isSilent = false;

    public Rigidbody dart;
    public GameObject bullet;
    public Transform dartOrigin, bulletOrigin;
    public WeaponSwitch weaponSwitch;
    public AudioSource fireSound;
    public AudioSource fullReloadSound;
    //public AudioSource startReloadSound;
    //public AudioSource loadBulletSound;
    //public AudioSource endReloadSound;
    public Animator animator;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash, aimedMuzzleFlash;
    public bool isFiring = false;

    private float nextTimeToFire = 0f;
    bool isReloading = false;
    bool isAiming = false;
    public MouseLook mouseLook;
    public CameraShake cameraShake;

    //For Ammo Count UI
    public GameObject currentAmmoUI;
    public GameObject maxAmmoUI;

    void Start()
    {
        currentAmmo = magazineAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
        animator.ResetTrigger("Firing");
    }
    void Update()
    {
        if ( !isEnemy )
        {
            if (isReloading)
            {
                return;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                animator.SetBool("isAiming", !animator.GetBool("isAiming"));
                StartCoroutine(WaitTime(1.5f));
                return;
            }

            if (currentAmmo <= 0 && maxAmmo <= 0)
            {
                // play blank fire sound
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.R) && currentAmmo == 0 && maxAmmo > 0)
            {
                StartCoroutine(FullReload());
                return;
            }

            /*else if (Input.GetKeyDown(KeyCode.R) && currentAmmo < magazineAmmo && maxAmmo > 0)
            {
                StartCoroutine(HotReload());
                return;
            }*/


            if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                if (currentAmmo > 0)
                {
                    if (isSilent && !animator.GetBool("isAiming"))
                    {
                        return;
                    }
                    Shoot();

                    if (!isReliable)
                    {
                        isActive = false;
                    }
                }
                else
                {
                    //StartCoroutine(HotReload());
                }
            }
        }

        if (isEnemy == false)
        {
            if (maxAmmo <= 0)
            {
                maxAmmoUI.GetComponent<Text>().text = "0";
                currentAmmoUI.GetComponent<Text>().text = currentAmmo.ToString();
            }
            else
            {
                currentAmmoUI.GetComponent<Text>().text = currentAmmo.ToString();
                maxAmmoUI.GetComponent<Text>().text = maxAmmo.ToString();
            }
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    IEnumerator HotReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isReloading", true);
        //startReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        while (currentAmmo < magazineAmmo && maxAmmo > 0)
        {
            //loadBulletSound.Play();
            maxAmmo -= 1;
            currentAmmo += 1;

            yield return new WaitForSeconds(bulletReload);
        }

        //endReloadSound.Play();
        yield return new WaitForSeconds(ReloadAnimationTime(weaponName));
        animator.SetBool("isReloading", false);
        isReloading = false;

    }

    IEnumerator FullReload()
    {
        isReloading = true;
        animator.SetBool("isReloading", true);
        animator.ResetTrigger("Firing");
        yield return new WaitForSeconds(0.25f);
        fullReloadSound.Play();
        yield return new WaitForSeconds(ReloadAnimationTime(weaponName));
        maxAmmo -= magazineAmmo;
        currentAmmo = magazineAmmo;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isReloading", false);
        isReloading = false;
    }
    void Shoot()
    {
        animator.ResetTrigger("Firing");
        GameObject bulletForward;
        Rigidbody dartForward;
        RaycastHit hit;
        ParticleSystem muzzle = selectMuzzle();

        isFiring = true;
        StartCoroutine(FireAnimation());
        currentAmmo -= 1;
        if (isSilent)
        {
            float speed = 50;

            dartForward = Instantiate(dart, dartOrigin.position, dartOrigin.rotation) as Rigidbody;
            dartForward.velocity = transform.TransformDirection(Vector3.forward * speed);
            return;
        }
        
        fireSound.Play();
        muzzle.Play();
        bulletForward = Instantiate(bullet, muzzle.GetComponentInParent<Transform>().position, fpsCamera.transform.rotation);
        bulletForward.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 500);
        if (Physics.Raycast(muzzle.GetComponentInParent<Transform>().position, fpsCamera.transform.forward, out hit, range) && !isSilent )
        {
            Debug.Log(hit.GetType());

            EnemyState enemy = hit.transform.GetComponent<EnemyState>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            /* GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
             * Destroy(impact, 0.5f);
             */
        }
        StartCoroutine(cameraShake.Shake(0.05f,0.5f));
        StartCoroutine(mouseLook.Recoil(1.5f, 0.25f));
    }

    public bool IsWeaponInLoadout()
    {
        return isActive;
    }

    private ParticleSystem selectMuzzle()
    {
        bool isAiming = animator.GetBool("isAiming");

        if (isAiming)
        {
            return aimedMuzzleFlash;
        }

        return muzzleFlash;
    }

    private float ReloadAnimationTime(string gun)
    {
        float result = 0;

        if (gun == "Paltik")
        {
            result = 5.0f;
        }
        else if (gun == "Sumpit")
        {
            result = 0f;
        }
        return result;
    }

    private IEnumerator FireAnimation()
    {
        animator.SetTrigger("Firing");
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Firing");

    }
    
}
