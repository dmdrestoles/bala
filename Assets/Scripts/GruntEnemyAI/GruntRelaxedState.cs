using UnityEngine;

public class GruntRelaxedState : GruntBaseState
{
    Vector3 noPatrol = new Vector3(0,0,0);
    Vector3 originalPos;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.awareness.awareRadius = 3f;
        this.originalPos = grunt.originalPos;
    }

    public override void SusDetected(GruntStateManager grunt)
    {
        if (grunt.awareness.susObject.name == "Muzzle")
        {
            grunt.susValue = 40;
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
        if (grunt.CheckForPlayertInLineOfSight(45, 20))
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
        else if ((grunt.aiMove_Utils.CheckDestinationReached(grunt.agent.transform.position, this.originalPos,1)))
        {
            //Debug.Log("Debug: "+grunt.gameObject.name + " Stopping Movement");
            grunt.aiMove_Utils.StopMovement(grunt.body,grunt.agent,grunt.animator);
        }
        else
        {
            grunt.agent.SetDestination(this.originalPos);
        }
    }
}