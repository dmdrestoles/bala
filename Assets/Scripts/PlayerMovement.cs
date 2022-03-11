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

    public Vector3 velocity, originalCamPos, oldPosition;
    public bool isGrounded;
    
    private int energy;
    private bool energyDraining = true;
    private bool energyGaining = true;
    public GameObject cameraHolder, playerBody;
    private float speed, maxCrouchHeight, crouchHeight;

    public AudioSource bushrustling;

    void Start()
    {
        energy = 6;
        speed = moveSpeed;
        originalCamPos = cameraHolder.transform.position;
        maxCrouchHeight = 0.55f;
        oldPosition = transform.position;
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
            
            if(Input.GetKeyUp(KeyCode.LeftControl) && !playerState.isCrouching)
            {
                playerState.isCrouching = true;
                playerState.isSprinting = false;
            }

            else if (crouchHeight < maxCrouchHeight && playerState.isCrouching)
            {
                cameraHolder.transform.position = new Vector3(
                    cameraHolder.transform.position.x, cameraHolder.transform.position.y- 0.2f, cameraHolder.transform.position.z);
                crouchHeight += 0.2f;
            }

            else if (Input.GetKeyUp(KeyCode.LeftControl) && playerState.isCrouching)
            {
                playerState.isCrouching = false;
            }

            else if (crouchHeight >= 0 && !playerState.isCrouching)
            {
                cameraHolder.transform.position = new Vector3(
                    cameraHolder.transform.position.x, cameraHolder.transform.position.y + 0.2f, cameraHolder.transform.position.z);
                crouchHeight -= 0.2f;
            }

            else if (Input.GetKey(KeyCode.LeftShift) && energy > 0)
            {
                playerState.isSprinting = true;
                playerState.isCrouching = false;
                animator.SetBool("isRunning", true);
                speed = moveSpeed * 1.5f;
                if (energyDraining)
                {
                    StartCoroutine(DrainEnergy());
                }
            }
            else if (energy < 1)
            {
                playerState.isSprinting = false;
                animator.SetBool("isRunning", false);
                if (energyGaining)
                {
                    StartCoroutine(GainEnergy());
                }
            }

            else if (playerState.isCrouching)
            {
                speed = moveSpeed * 0.5f;
            }
            
            else
            {
                playerState.isSprinting = false;
                animator.SetBool("isRunning", false);
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
        
        if (Input.GetButton("Vertical") || Input.GetButton("Horizontal"))
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
            playerState.isVisible = true;
        }
    }

    public bool checkVisibility()
    {
        return playerState.isVisible;
    }

    IEnumerator PlaySound(AudioSource audio)
    {
        audio.Play();
        yield return new WaitWhile( () => audio.isPlaying );
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
