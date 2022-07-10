using UnityEngine;

public abstract class GruntBaseState 
{
    public abstract void EnterState(GruntStateManager grunt);    
    public abstract void UpdateState(GruntStateManager grunt);
    public abstract void SusDetected(GruntStateManager grunt);
}