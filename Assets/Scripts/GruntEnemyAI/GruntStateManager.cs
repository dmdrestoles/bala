using System.Collections;
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
    public float health = 50;
    public int maxAmmo = 5;
    public bool isDead = false;
    public bool isAsleep = false;
    public GruntRelaxedState relaxedState = new GruntRelaxedState();
    public GruntPatrollingState patrollingState = new GruntPatrollingState();
    public GruntSuspiciousState suspiciousState = new GruntSuspiciousState();
    public GruntHuntingState huntingState = new GruntHuntingState();
    public GruntAimingState aimingState = new GruntAimingState();
    public GruntReloadingState reloadingState = new GruntReloadingState();
    public GruntFiringState firingState = new GruntFiringState();
    public GruntDeathState deathState = new GruntDeathState();
    public GruntSleepState sleepState = new GruntSleepState();
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
    bool AlreadyAsleep = false;
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
        if (Vector3.Distance(playerTransform.position,this.transform.position) <=250)
        {
            agent.enabled = true;
            HandleSleep();
            HandleDeath();
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
                //ForgetSus();
            }
        }
        else
        {
            agent.enabled = false;
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
        //Keep Last known Position
        if (susObject == null && this.susPos !=null)
        {}
        else if (susObject !=null )
        {
            this.susPos = susObject.transform.position;
        }

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
        if ( susValue <= 0 && awareIndi.activeSelf)
        {
            awareIndi.SetActive(false);
        }
        else if(susValue > 0 && currentState != deathState)
        {
            awareIndi.SetActive(true);
            awareIndi.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, susValue/50);
            awareIndi.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(Color.green, Color.red, susValue/50));
            //Debug.Log("Debug: "+awareIndi.GetComponent<Renderer>().material.color.ToString() );
        }
    }

    void HandleDeath()
    {
        if (this.health <= 0 && !isDead)
        {
            this.isDead = true;
            this.SwitchState(this.deathState);
        }
    }

    void HandleSleep()
    {
        /*
        isAsleep is a public variable that can be controlled by other gameobjects
        AlreadyAsleep is a private var which makes sure that this method doesnt fire
        multiple times
        */
        if (this.isAsleep && !this.AlreadyAsleep)
        {
            this.AlreadyAsleep = true;
            StartCoroutine(GoToSleep());
        }
    }
    IEnumerator GoToSleep()
    {
        this.SwitchState(this.sleepState);
        yield return new WaitForSeconds(20.0f);
        this.animator.SetTrigger("triggerAwake");
        yield return new WaitForSeconds(1.0f);
        this.isAsleep = false;
        this.AlreadyAsleep = false;
        this.animator.ResetTrigger("triggerAwake");
        this.SwitchState(this.relaxedState);

    }
    IEnumerator PlaySound(AudioSource audio, float buffer)
    {
        yield return new WaitForSeconds(buffer);
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
    }

    IEnumerator WaitForALongTime()
    {
        yield return new WaitForSeconds(1000000.0f);
    }
}