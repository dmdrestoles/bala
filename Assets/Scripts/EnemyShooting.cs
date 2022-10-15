using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject muzzle, player, bullet;
    public AudioSource fireSound, fullReloadSound, startReloadSound, attackSound;
    public AudioSource loadBulletSound, endReloadSound;
    public ParticleSystem muzzleFlash;
    public int magazineAmmo = 5;
    public int currentAmmo = 5;
    public float bulletReload = 0.5f;
    [HideInInspector]
    private EnemyState state;
    private EnemyMovement movement;
    private float waitTime = 2.0f;
    private float timer = 0.0f;
    private Transform target;
    private bool isReloading;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        state = GetComponent<EnemyState>();
        movement = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsInputEnabled)
        {
            target = player.transform;
            if (!state.isAsleep)
            {
                HandleShooting();
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        GameObject bulletForward;
        
        StartCoroutine(ShootAnimation());

        bool shootCast = Physics.Linecast(muzzle.transform.position, CalculateMiss(target.position), out hit);
        bulletForward = Instantiate(bullet, muzzle.transform.position, muzzle.transform.rotation);
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

        if (state.isFiring)
        {
            state.isAiming = true;
            animator.SetBool("isAiming", true);
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
        double missFactor = System.Math.Pow(2, 0.01*(double)state.distanceFromPlyaer) - 1;
        float a = (float)missFactor;

        float xRand = Random.Range(-a, a);
        float yRand = Random.Range(-a, a);
        float zRand = Random.Range(-a, a);

        target.x += xRand;
        target.y += yRand;
        target.z += zRand;

        return target;
    }

    IEnumerator PlaySound(AudioSource audio)
    {
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
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
        movement.StopMovement();
        animator.SetTrigger("triggerFire");
        fireSound.Play();
        muzzleFlash.Play();
        yield return new WaitForSeconds(0.15f);
        animator.ResetTrigger("triggerFire");
        movement.ResumeMovement();
        animator.SetBool("isAiming", false);
    }
}
