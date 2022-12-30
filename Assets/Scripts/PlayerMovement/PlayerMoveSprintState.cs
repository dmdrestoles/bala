using UnityEngine;

public class PlayerMoveSprintState : PlayerMoveBaseState
{
    PlayerMoveStateManager playerMovement;
    float elapsed = 0f;

    public override void EnterState(PlayerMoveStateManager stateManager)
    {
        playerMovement = stateManager;
        playerMovement.animator.SetBool("isRunning", true);
        playerMovement.animator.SetBool("isCrouching", false);
        UpdateFootStepsRad();
        UpdateSpeedMultiplier();
    }
    public override void UpdateState(PlayerMoveStateManager stateManager)
    {
        RunEverySecond();
        HandleCrouchingMovement();
        if (Input.GetKeyUp(KeyCode.LeftShift) || stateManager.energy <= 0)
        {
            playerMovement.animator.SetBool("isRunning", false);
            stateManager.SwitchState(stateManager.walkState);
        } 
    }

    public override void UpdateFootStepsRad()
    {
        playerMovement.footSteps.radius = 10;
    }

    public override void UpdateSpeedMultiplier()
    {
        playerMovement.speed = playerMovement.moveSpeed * 1.5f;
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
    void RunEverySecond()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            DrainEnergy();
            //Debug.Log("Debug: " +"Energy:" + playerMovement.energy);
        }
    }
    void DrainEnergy()
    {
        if (playerMovement.energy > 0 )
        {
            playerMovement.energy--;
        }
    }
}