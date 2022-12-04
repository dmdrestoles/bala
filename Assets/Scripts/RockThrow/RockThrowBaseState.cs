using UnityEngine;

public abstract class RockThrowBaseState
{
    public abstract void EnterState(RockThrowStateManager stateManager);    
    public abstract void UpdateState(RockThrowStateManager stateManager);
}