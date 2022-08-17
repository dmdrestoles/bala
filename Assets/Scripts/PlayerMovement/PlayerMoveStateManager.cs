using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveStateManager : MonoBehaviour
{
    public float moveSpeed = 20f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;
    public float maxCrouchHeight = 0.55f;
    public int energy = 6;
    public Transform groundCheck;
    public CapsuleCollider footSteps;
    public AudioSource bushrustling;
    public GameObject cameraHolder, playerBody;
    public PlayerMoveBaseState currentState;
    public PlayerMoveWalkState WalkState = new PlayerMoveWalkState();
    CharacterController controller;
    LayerMask groundMask;
    Vector3 velocity, originalCamPos;
    bool isGrounded;
    float groundDistance = 0.4f;
    float speed;
    float crouchHeight;
    bool energyDraining = true;
    bool energyGaining = true;
    void Start()
    {
        speed = moveSpeed;
        originalCamPos = cameraHolder.transform.position;
        controller = this.GetComponent<CharacterController>();

        currentState = WalkState;
        currentState.EnterState(this);
    }

    void Update()
    {
        if (GameManager.IsInputEnabled == true)
        {
            currentState.UpdateState(this);
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}