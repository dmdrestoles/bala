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
    
    private int energy;
    private bool energyDraining = true;
    private bool energyGaining = true;
    public GameObject cameraHolder, playerBody;
    private float speed, maxCrouchHeight, crouchHeight;

    void Start()
    {
        energy = 6;
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
            
            if(Input.GetKey(KeyCode.LeftControl))
            {
                playerState.isCrouching = true;
                playerState.isSprinting = false;
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
                playerState.isCrouching = false;
                cameraHolder.transform.position = new Vector3(
                    cameraHolder.transform.position.x, cameraHolder.transform.position.y + crouchHeight, cameraHolder.transform.position.z);
                crouchHeight = 0;
            }

            else if (Input.GetKey(KeyCode.LeftShift) && energy > 0)
            {
                playerState.isSprinting = true;
                playerState.isCrouching = false;
                // animator.SetBool("Running", true);
                speed = moveSpeed * 1.5f;
                if (energyDraining)
                {
                    StartCoroutine(DrainEnergy());
                }
            }
            else if (energy < 1)
            {
                playerState.isSprinting = false;
                if (energyGaining)
                {
                    StartCoroutine(GainEnergy());
                }
            }
            
            else
            {
                playerState.isSprinting = false;
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
        if (other.gameObject.tag == "HidingSpot" && playerState.isCrouching)
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

    IEnumerator DrainEnergy()
    {
        energyDraining = false;
        energy--;
        yield return new WaitForSeconds(1f);
        energyDraining = true;
    }

    IEnumerator GainEnergy()
    {
        energyGaining = false;
        yield return new WaitForSeconds(6f);
        energy = 6;
        energyGaining = true;
    }
}
