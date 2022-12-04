using System.Collections;
using UnityEngine;

public class PlayerMoveIdleState : PlayerMoveBaseState
{
    PlayerMoveStateManager playerMovement;
    float elapsed = 0f;
    public override void EnterState(PlayerMoveStateManager stateManager)
    {
        playerMovement = stateManager;
        playerMovement.animator.SetBool("isMoving", false);
        playerMovement.animator.SetBool("isRunning", false);
        UpdateFootStepsRad();
        UpdateSpeedMultiplier();
    }
    public override void UpdateState(PlayerMoveStateManager stateManager)
    {
        HandleCrouchingMovement();
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            stateManager.SwitchState(stateManager.crouchState);
        }
        else if (stateManager.move.magnitude > 0)
        {
            stateManager.SwitchState(stateManager.walkState);
        }
    }

    public override void HandleCrouchingMovement()
    {
        if (playerMovement.crouchHeight >= 0)
        {
            playerMovement.cameraHolder.transform.position = new Vector3
            (
                playerMovement.cameraHolder.transform.position.x, 
                playerMovement.cameraHolder.transform.position.y + 0.2f, 
                playerMovement.cameraHolder.transform.position.z
            );
            playerMovement.crouchHeight -= 0.2f;
        }
    }

    public override void UpdateFootStepsRad()
    {
        playerMovement.footSteps.radius = 4;
    }

    public override void UpdateSpeedMultiplier()
    {
        playerMovement.speed = playerMovement.moveSpeed;
    }

    void GainEnergy()
    {
        if (playerMovement.energy < 6)
        {
            playerMovement.energy++;
        }
    }
}