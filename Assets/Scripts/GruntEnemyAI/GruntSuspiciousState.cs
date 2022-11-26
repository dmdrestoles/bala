using System.Collections;
using UnityEngine;

public class GruntSuspiciousState : GruntBaseState
{
    GruntStateManager grunt;
    float elapsed = 0f;
    public override void EnterState(GruntStateManager stateManager)
    {
        this.grunt = stateManager;
        this.grunt.aiMove_Utils.ResumeMovement(grunt.body,grunt.agent,grunt.animator);
        this.grunt.animator.SetBool("isWalking", true);
        this.grunt.agent.speed = 3f;
        this.grunt.awareness.awareRadius = 5f;
    }

    public override void SusDetected()
    {
        if (this.grunt.CheckForPlayertInLineOfSight(45, 20))
        {
            this.grunt.susValue = 50;
            this.grunt.SwitchState(this.grunt.huntingState);
        }
        if (this.grunt.awareness.susObject != null)
        {
            if (this.grunt.awareness.susObject.name == "Muzzle")
            {
                this.grunt.susValue +=20;
                //this.grunt.SwitchState(grunt.huntingState);
            } 
            else if (this.grunt.awareness.susObject.name == "FootSteps")
            {
                this.grunt.susValue +=5;
                //this.grunt.SwitchState(grunt.suspiciousState);
            }
            else if (grunt.awareness.susObject.name == "Rock")
            {
                this.grunt.susValue +=5;
                //this.grunt.SwitchState(grunt.suspiciousState);
            }
        }
        else
        {
            this.grunt.susValue -=4;
        }

    }

    public override void UpdateState(GruntStateManager stateManager)
    {
        RunEverySecond();
        if (this.grunt.susValue >= 45)
        {
            this.grunt.SwitchState(this.grunt.huntingState);
        }
        else if (this.grunt.susValue <= 0)
        {
            this.grunt.SwitchState(grunt.relaxedState);
        }
        else if (this.grunt.susPos != new Vector3(0,0,0))
        {
            GoToSus();
        }
    }

    void GoToSus()
    {
        if (this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position, this.grunt.susPos, 2))
        {
            this.grunt.aiMove_Utils.StopMovement(this.grunt.body, this.grunt.agent, this.grunt.animator);
        }
        else
        {
            this.grunt.aiMove_Utils.ResumeMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
            this.grunt.agent.SetDestination(this.grunt.susPos);
            this.grunt.animator.SetBool("isMoving", true);
        }
    }

    void RunEverySecond()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            SusDetected();
            Debug.Log("Debug: " + this.grunt.susValue);
        }
    }
}