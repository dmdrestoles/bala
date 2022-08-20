using UnityEngine;

public class PlayerMoveJumpState : PlayerMoveBaseState
{
    PlayerMoveStateManager playerMovement;

    public override void EnterState(PlayerMoveStateManager stateManager)
    {
        playerMovement = stateManager;
        stateManager.velocity.y = Mathf.Sqrt(stateManager.jumpHeight * -2f * stateManager.gravity);
        UpdateFootStepsRad();
    }

    public override void UpdateState(PlayerMoveStateManager stateManager)
    {
        if (stateManager.isGrounded)
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
        playerMovement.footSteps.radius = 11;
    }

    //No change in speed when jumping
    public override void UpdateSpeedMultiplier(){}

}