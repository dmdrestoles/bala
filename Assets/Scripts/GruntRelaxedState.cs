using UnityEngine;

public class GruntRelaxedState : GruntBaseState
{
    Vector3 noPatrol = new Vector3(0,0,0);
    public override void EnterState(GruntStateManager grunt)
    {
        
    }

    public override void SusDetected(GruntStateManager grunt)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        if (grunt.patrol1 != noPatrol && grunt.patrol2 != noPatrol)
        {
            grunt.SwitchState(grunt.patrollingState);
        }
    }
}