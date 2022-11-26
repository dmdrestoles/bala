using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveStateManager : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float gravity = 0;
    public float jumpHeight = 4.0f;
    public float maxCrouchHeight = 0.55f;
    public int energy = 6;
    public float speed;
    public Transform groundCheck;
    public CapsuleCollider footSteps;
    public AudioSource bushrustling;
    public GameObject cameraHolder, playerBody;
    public float crouchHeight;
    public bool isGrounded;
    public Vector3 velocity;
    public PlayerMoveBaseState currentState;
    public PlayerMoveWalkState walkState = new PlayerMoveWalkState();
    public PlayerMoveSprintState sprintState = new PlayerMoveSprintState();
    public PlayerMoveCrouchState crouchState = new PlayerMoveCrouchState();
    public PlayerMoveJumpState jumpState = new PlayerMoveJumpState();
    CharacterController controller;
    public Animator animator;
    public LayerMask groundMask;
    Vector3 originalCamPos;
    float groundDistance = 0.2f;
    bool energyDraining = true;
    bool energyGaining = true;
    void Start()
    {
        speed = moveSpeed;
        originalCamPos = cameraHolder.transform.position;
        controller = this.GetComponent<CharacterController>();

        currentState = walkState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (GameManager.IsInputEnabled == true)
        {
            currentState.UpdateState(this);
            HandleJump();
            HandleMovement();
        }
    }

    //Handles movement using the wasd keys
    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Debug.Log("Debug: isGrounded: "+ isGrounded);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        StartCoroutine(HandleJump());
        //Debug.Log("Debug: " + "Vector Move of Player: " + move);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }

    IEnumerator HandleJump()
    {
        if ((Input.GetButtonDown("Jump") && this.currentState != jumpState) )
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            yield return new WaitUntil(() => !isGrounded);
            this.SwitchState(this.jumpState);
        }
    }

    public void SwitchState(PlayerMoveBaseState state)
    {
        currentState = state;
        //Debug.Log("Debug: " + currentState.ToString());
        state.EnterState(this);
    }

    // Folowing Methods handle when player is in a bush. note: invisibility not yet implemented
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot" && this.currentState == crouchState)
        {
            //playerState.isVisible = false;
        }
        else
        {
            //playerState.isVisible = true;
        }
        
        if ((Input.GetButton("Vertical") || Input.GetButton("Horizontal")) && other.gameObject.tag == "HidingSpot")
        {
            if (!bushrustling.isPlaying)
            {
                StartCoroutine(PlaySound(bushrustling));
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
           //playerState.isVisible = true;
        }
    }

    public bool checkVisibility()
    {
        return true; //playerState.isVisible;
    }

    IEnumerator PlaySound(AudioSource audio)
    {
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
    }
}