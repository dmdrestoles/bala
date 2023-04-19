using UnityEngine.SceneManagement;
using UnityEngine;                      

public class PickupStateManager : MonoBehaviour
{
    [Header("Object Info")]
    public string objectName;
    public bool isMain, isPrimary, isSecondary, isMelee;

    [Header("States")]
    public PickupBaseState currentState;
    public PickupDebugState debugState;
    public PickupGlowingState glowingState;
    public PickupRestState restState;

    [Header("Object References")]
    public MouseLook mouseLook;
    public GameObject codexNotif;
    public GameObject codex;
    public Sprite sprite;
    public WeaponManager weaponManager;
    public GameObject primaryInv, secondaryInv, meleeInv;
    public AudioSource pickupSFX;
    
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        gameObject.AddComponent<PickupDebugState>();
        gameObject.AddComponent<PickupGlowingState>();
        gameObject.AddComponent<PickupRestState>();

        debugState = gameObject.GetComponent<PickupDebugState>();
        glowingState = gameObject.GetComponent<PickupGlowingState>();
        restState = gameObject.GetComponent<PickupRestState>();

        player = GameObject.Find("Player");

        if (isMain)
        {
            currentState = debugState;
        }
        else
        {
            currentState = restState;
        }

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
        //HandleObjectFallThroughFloor();
    }

    public void Transition(PickupBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public bool IsPlayerNear(){
        return Vector3.Distance(player.transform.position, gameObject.transform.position) <= 25;
    }

    public bool OnMouseOver(){
        return mouseLook.GetSelectedObject() == gameObject.name;
    }

    void HandleObjectFallThroughFloor()
    {
        if(gameObject.transform.position.y < 0f && SceneManager.GetActiveScene().name == "Jared-Level-5-Scene-Improvments")
        {
            Debug.Log( "Debug: "+ gameObject.name+" FALLS THROUGH " + transform.position.y);
            transform.position = new Vector3(transform.position.x, transform.position.y+ 3.0f, transform.position.z);
            //transform.rotation = new Quaternion(0,0,0,0);
        }
    }

    /*
    public virtual void HandleObjectPickup(string objectName)
    {
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    */
}
