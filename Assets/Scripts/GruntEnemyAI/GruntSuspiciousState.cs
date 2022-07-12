using System.Collections;
using UnityEngine;

public class GruntSuspiciousState : GruntBaseState
{
    float elapsed = 0f;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.aiMove_Utils.ResumeMovement(grunt.body,grunt.agent,grunt.animator);
        grunt.animator.SetBool("isWalking", true);
        grunt.agent.speed = 3f;
        grunt.awareness.awareRadius = 5f;
    }

    public override void SusDetected(GruntStateManager grunt)

    {
        AddSusLevel(grunt);

        if (grunt.aiMove_Utils.CheckDestinationReached(grunt.agent.transform.position, grunt.susPos, 5))
        {
            grunt.aiMove_Utils.StopMovement(grunt.body, grunt.agent, grunt.animator);
        }
        else if (grunt.CheckForPlayertInLineOfSight(45, 20))
        {
            grunt.susValue = 50;
            grunt.SwitchState(grunt.huntingState);
        }
        else
        {
            grunt.aiMove_Utils.ResumeMovement(grunt.body,grunt.agent,grunt.animator);
            grunt.agent.SetDestination(grunt.susPos);
            grunt.animator.SetBool("isMoving", true);
        }
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        if (grunt.susValue >= 35)
        {
            grunt.SwitchState(grunt.huntingState);
        }
        else if (grunt.susPos != new Vector3(0,0,0))
        {
            SusDetected(grunt);
        }
        else if(grunt.susPos == new Vector3(0,0,0) )
        {
            grunt.susValue = 0;
            grunt.SwitchState(grunt.relaxedState);
        }
    }

    void AddSusLevel(GruntStateManager grunt)
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            grunt.susValue += 1;
            Debug.Log("Debug: " + grunt.susValue);
        }
    }

}