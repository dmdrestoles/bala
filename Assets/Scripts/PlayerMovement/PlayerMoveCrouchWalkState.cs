using System.Collections;
using UnityEngine;

public class PlayerMoveCrouchWalkState : PlayerMoveBaseState
{
    PlayerMoveStateManager playerMovement;
    float elapsed = 0f;
    public override void EnterState(PlayerMoveStateManager stateManager)
    {
        playerMovement = stateManager;
        playerMovement.animator.SetBool("isCrouching", true);
        playerMovement.animator.SetBool("isMoving", true);
        playerMovement.animator.SetBool("isRunning", false);
        UpdateFootStepsRad();
        UpdateSpeedMultiplier();
    }
    public override void UpdateState(PlayerMoveStateManager stateManager)
    {
        RunEverySecond();
        HandleCrouchingMovement();
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            stateManager.SwitchState(stateManager.walkState);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && stateManager.energy >4)
        {
            stateManager.SwitchState(stateManager.sprintState);
        }
    }
    public override void UpdateFootStepsRad()
    {
        playerMovement.footSteps.radius = 2.0f;
    }

    public override void UpdateSpeedMultiplier()
    {
        playerMovement.speed = playerMovement.moveSpeed * 0.25f;
    }

    public override void HandleCrouchingMovement()
    {
        if (playerMovement.crouchHeight < playerMovement.maxCrouchHeight)
        {
            playerMovement.cameraHolder.transform.position = new Vector3
            (
                playerMovement.cameraHolder.transform.position.x, 
                playerMovement.cameraHolder.transform.position.y - 0.2f, 
                playerMovement.cameraHolder.transform.position.z
            );
            playerMovement.crouchHeight += 0.2f;
        }
    }

    void RunEverySecond()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            GainEnergy();
            //Debug.Log("Debug: " +"Energy:" + playerMovement.energy);
        }
    }

    void GainEnergy()
    {
        if (playerMovement.energy < 6)
        {
            playerMovement.energy++;
        }
    }
}