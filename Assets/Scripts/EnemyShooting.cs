using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public EnemyState state;
    public GameObject gun;
    public GameObject player;
    public float range;
    public AudioSource fireSound, fullReloadSound, startReloadSound;
    public AudioSource loadBulletSound, endReloadSound;
    public ParticleSystem muzzleFlash;
    public int magazineAmmo = 5;
    public int currentAmmo = 5;
    public float bulletReload = 0.5f;
    private float waitTime = 4.1f;
    private float timer = 0.0f;
    private Transform target;
    private bool isReloading;
    // Start is called before the first frame update
    void Start()
    {
        
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
        bool shootCast = Physics.Linecast(gun.transform.position, CalculateMiss(target.position), out hit);
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

        if (timer > waitTime && state.isPlayerDetected)
        {
            // Remove the recorded 2 seconds.
            timer = timer - waitTime;
            Shoot();
            currentAmmo-=1;
            fireSound.Play();
            muzzleFlash.Play();
        }
    }

    Vector3 CalculateMiss(Vector3 target){
        double missFactor = System.Math.Pow(2, 0.08*(double)state.distanceFromPlyaer) - 1;
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
}
