using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public PlayerState playerState;
    public Animator animator;
    
    public static float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 3.0f;

    public Vector3 velocity, originalCamPos;
    public bool isGrounded;
    public GameObject cameraHolder, playerBody;
    private float speed, maxCrouchHeight, crouchHeight;

    void Start()
    {
        speed = moveSpeed;
        originalCamPos = cameraHolder.transform.position;
        maxCrouchHeight = 0.68f;
    }
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (GameManager.IsInputEnabled == true)
        {
            Vector3 move = transform.right * x + transform.forward * z;

            if ( move.magnitude <= 0.001f )
            {
                // animator.SetBool("Moving", false);
            }
            else
            {
                // animator.SetBool("Moving", true);
            }
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                // animator.SetBool("Running", true);
                speed = moveSpeed * 1.5f;
            }
            else if(Input.GetKey(KeyCode.LeftControl))
            {
                speed = moveSpeed * 0.5f;
                if (crouchHeight < maxCrouchHeight)
                {
                    cameraHolder.transform.position = new Vector3(
                        cameraHolder.transform.position.x, cameraHolder.transform.position.y- 0.1f, cameraHolder.transform.position.z);
                    crouchHeight += 0.1f;
                }
               
            }
            else if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                cameraHolder.transform.position = new Vector3(
                    cameraHolder.transform.position.x, cameraHolder.transform.position.y + crouchHeight, cameraHolder.transform.position.z);
                crouchHeight = 0;
            }
            
            else
            {
                speed = moveSpeed;
            }

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            playerState.isVisible = false;
        }
        else
        {
            playerState.isVisible = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "HidingSpot")
        {
            playerState.isVisible = true;
        }
    }

    public bool checkVisibility()
    {
        return playerState.isVisible;
    }
}
