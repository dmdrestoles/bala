using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GruntStateManager : MonoBehaviour
{
    public Transform playerTransform;
    public GameObject susObject, awareIndi;
    public GameObject muzzle, bullet;
    public float color;
    public Vector3 susPos;
    public GruntBaseState currentState;
    public float susValue = 0;
    public int currentAmmo = 0;
    public int maxAmmo = 5;
    public GruntRelaxedState relaxedState = new GruntRelaxedState();
    public GruntPatrollingState patrollingState = new GruntPatrollingState();
    public GruntSuspiciousState suspiciousState = new GruntSuspiciousState();
    public GruntHuntingState huntingState = new GruntHuntingState();
    public GruntAimingState aimingState = new GruntAimingState();
    public GruntReloadingState reloadingState = new GruntReloadingState();
    public GruntFiringState firingState = new GruntFiringState();
    public PlayerState playerState;
    public AudioSource reloadAud, fireAud;
    public ParticleSystem muzzleFlash;
    public Vector3 patrol1, patrol2, originalPos;
    public Detection_Utils detect_Utils;
    public AIMovement_Utils aiMove_Utils;
    public GruntAwareness awareness;
    public NavMeshAgent agent;
    public Animator animator;
    public Rigidbody body;
    float elapsed= 0f;
    RaycastHit hit;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody>();
        currentState = relaxedState;
        currentState.EnterState(this);

        originalPos = transform.position;
    }

    void Update()
    {
        currentState.UpdateState(this);
        UpdateAwareColor();
        if (awareness.susObject)
        {
            this.susObject = awareness.susObject;
            UpdateSusPos();
            //Debug.Log("Debug: SusPos: " + susPos);
            //Debug.Log("Debug: SusObject Detected");
        }
        else
        {
            this.susObject = null;
            ForgetSus();
        }
    }

    public void SwitchState(GruntBaseState state)
    {
        currentState = state;
        Debug.Log("Debug: " + currentState.ToString());
        state.EnterState(this);
    }

    public bool CheckForPlayertInLineOfSight(float angle, float distance)
    {
        Vector3 selfPos = new Vector3(this.transform.position.x , this.transform.position.y + 3, this.transform.position.z);
        bool result = false;
        if (Physics.Linecast(selfPos, this.playerTransform.position, out hit))
        {
            Debug.DrawLine(selfPos, this.playerTransform.position, Color.green);
            if (hit.transform.CompareTag("Player") && this.detect_Utils.IsHitWithinObjectAngle(hit, this.transform, angle)
                    && this.detect_Utils.IsHitWithinObjectDistance(hit, distance))
            {
               result = true;
            }
        }
        return result;        
    }

    void ForgetSus()
    {
        if (susPos != new Vector3(0,0,0))
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 10f) {
                elapsed = elapsed % 10f;
                this.susPos = new Vector3(0,0,0);
                //Debug.Log("Debug: SusPos: " + susPos);
                //Debug.Log("Debug: Forgetting SusObject Transform");
            }
        }
    }

    void UpdateSusPos()
    {
        this.susPos = susObject.transform.position;
    }

    public void playReload()
    {
        StartCoroutine(PlaySound(reloadAud,1f));
    }

    public void playFire()
    {
        StartCoroutine(PlaySound(fireAud,0.2f));
        muzzleFlash.Play();
    }

    public GameObject SpawnBullet()
    {
        return Instantiate(this.bullet, this.muzzle.transform.position, this.muzzle.transform.rotation);
    }

    void UpdateAwareColor()
    {
        awareIndi.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, susValue/50);
        awareIndi.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green, Color.red, susValue/50));
    }

    IEnumerator PlaySound(AudioSource audio, float buffer)
    {
        yield return new WaitForSeconds(buffer);
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
    }
}