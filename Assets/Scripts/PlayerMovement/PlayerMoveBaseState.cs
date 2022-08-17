using UnityEngine;

public abstract class PlayerMoveBaseState
{
    public abstract void EnterState(PlayerMoveStateManager stateManager);    
    public abstract void UpdateState(PlayerMoveStateManager stateManager);
    public abstract void UpdateFootStepsRad();
    public abstract void UpdateSpeedMultiplier();
}