using UnityEngine;

public class GruntHuntingState : GruntBaseState
{
    float elapsed = 0f;
    RaycastHit hit;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetBool("isAiming", false);
        grunt.animator.SetBool("isMoving", true);
        grunt.animator.SetBool("isWalking", false);
        grunt.agent.speed = 8f;
    }

    public override void SusDetected(GruntStateManager grunt)
    {
        AddSusLevel(grunt);

        if (grunt.aiMove_Utils.CheckDestinationReached(grunt.agent.transform.position, grunt.susPos,6))
        {
            grunt.aiMove_Utils.StopMovement(grunt.body, grunt.agent, grunt.animator);
        }
        else
        {
            grunt.aiMove_Utils.ResumeMovement(grunt.body,grunt.agent,grunt.animator);
            grunt.agent.SetDestination(grunt.susPos);
            grunt.animator.SetBool("isMoving", true);
            grunt.animator.SetBool("isWalking", false);
        }
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        if (grunt.susValue < 35)
        {
            grunt.SwitchState(grunt.suspiciousState);
        }
        else if (grunt.CheckForPlayertInLineOfSight(60, 12))
        {
            grunt.SwitchState(grunt.aimingState);
        }
        else if (grunt.susPos != new Vector3(0,0,0))
        {
            SusDetected(grunt);
        }
        else
        {
            this.SubSusLevel(grunt);
        }

    }

    void AddSusLevel(GruntStateManager grunt)
    {
        if (grunt.susValue <= 50)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 1f) {
                elapsed = elapsed % 1f;
                grunt.susValue += 1;
                Debug.Log("Debug: " + grunt.susValue);
            }
        }
    }

    void SubSusLevel(GruntStateManager grunt)
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            grunt.susValue -= 1 + elapsed;
            //Debug.Log("Debug: " + grunt.susValue);
        }
    }

}