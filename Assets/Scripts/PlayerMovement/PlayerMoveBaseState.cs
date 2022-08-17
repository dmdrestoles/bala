using UnityEngine;

public abstract class PlayerMoveBaseState
{
    public abstract void EnterState(PlayerMoveStateManager stateManager);    
    public abstract void UpdateState(PlayerMoveStateManager stateManager);
    //Handles updating the radius of the footsteps capsule collider.
    public abstract void UpdateFootStepsRad();
    //Handles updating the speed.
    public abstract void UpdateSpeedMultiplier();
    //Handles raising and lowering of camera when crouching.
    public abstract void HandleCrouchingMovement();
}