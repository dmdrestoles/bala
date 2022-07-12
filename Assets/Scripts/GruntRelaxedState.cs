using UnityEngine;

public class GruntRelaxedState : GruntBaseState
{
    Vector3 noPatrol = new Vector3(0,0,0);
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.awareness.awareRadius = 3f;
    }

    public override void SusDetected(GruntStateManager grunt)
    {
        if (grunt.awareness.susObject.name == "Muzzle")
        {
            grunt.susValue = 50;
            grunt.SwitchState(grunt.huntingState);
        } 
        else if (grunt.awareness.susObject.name == "FootSteps")
        {
            grunt.susValue +=30;
            grunt.SwitchState(grunt.suspiciousState);
        }
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        if (grunt.CheckForPlayertInLineOfSight(45, 15))
        {
            grunt.susValue = 50;
            grunt.SwitchState(grunt.huntingState);
        }
        else if (grunt.susObject)
        {
            SusDetected(grunt);
        }
        else if (grunt.patrol1 != noPatrol && grunt.patrol2 != noPatrol)
        {
            grunt.SwitchState(grunt.patrollingState);
        }
    }
}