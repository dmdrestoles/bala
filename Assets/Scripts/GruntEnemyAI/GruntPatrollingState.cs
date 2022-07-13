using UnityEngine;
using UnityEngine.AI;


public class GruntPatrollingState : GruntBaseState
{
    bool patrolStarted = false;


    public override void EnterState(GruntStateManager grunt)
    {
        patrolStarted = false;
        grunt.awareness.awareRadius = 3f;
        grunt.aiMove_Utils.ResumeMovement(grunt.body,grunt.agent,grunt.animator);
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
        Debug.Log(grunt.CheckForPlayertInLineOfSight(45, 20));
        if (grunt.CheckForPlayertInLineOfSight(45, 20))
        {
            grunt.susValue = 50;
            grunt.SwitchState(grunt.huntingState);
        }
        else if (grunt.susObject)
        {
            SusDetected(grunt);
        }
        Patrol(grunt.patrol1, grunt.patrol2, grunt);
    }

    void Patrol(Vector3 start, Vector3 end, GruntStateManager grunt){
        grunt.animator.SetBool("isMoving", true);
        grunt.animator.SetBool("isWalking", true);
        grunt.agent.speed = 3.0f;
        if ((grunt.aiMove_Utils.CheckDestinationReached(grunt.agent.transform.position, start,1)) && !grunt.agent.pathPending )
        {
            grunt.agent.SetDestination(end);
        }
        else if ((grunt.aiMove_Utils.CheckDestinationReached(grunt.agent.transform.position,end,1)) && !grunt.agent.pathPending )
        {
            grunt.agent.SetDestination(start);
        }
        else if(!patrolStarted)
        {
            patrolStarted = true;
            grunt.agent.SetDestination(start);
        }
    }
}