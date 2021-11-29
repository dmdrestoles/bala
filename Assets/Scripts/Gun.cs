using System.Collections;
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
    public AudioSource startReloadSound;
    public AudioSource loadBulletSound;
    public AudioSource endReloadSound;
    public Animator animator;

    public Camera fpsCamera;
    public ParticleSystem muzzleFlash, aimedMuzzleFlash;
    public bool isFiring = false;

    private float nextTimeToFire = 0f;
    private bool isReloading = false;

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
    }
    void Update()
    {
        if ( !isEnemy )
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
                    StartCoroutine(HotReload());
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

    IEnumerator HotReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("isReloading", true);
        startReloadSound.Play();
        yield return new WaitForSeconds(0.5f);
        while (currentAmmo < magazineAmmo && maxAmmo > 0)
        {
            loadBulletSound.Play();
            maxAmmo -= 1;
            currentAmmo += 1;

            yield return new WaitForSeconds(bulletReload);
        }

        endReloadSound.Play();
        yield return new WaitForSeconds(ReloadAnimationTime(weaponName));
        animator.SetBool("isReloading", false);
        isReloading = false;

    }
    void Shoot()
    {
        GameObject bulletForward;
        Rigidbody dartForward;
        RaycastHit hit;
        ParticleSystem muzzle = selectMuzzle();

        isFiring = true;
        animator.SetTrigger("Firing");
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
            result = 3.5f;
        }
        else if (gun == "Sumpit")
        {
            result = 0f;
        }
        return result;
    }
    
}
