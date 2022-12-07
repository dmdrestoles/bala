using UnityEngine;

public class GruntHuntingState : GruntBaseState
{
    GruntStateManager grunt;
    float elapsed = 0f;
    RaycastHit hit;
    public override void EnterState(GruntStateManager stateManager)
    {
        this.grunt = stateManager;
        this.grunt.animator.SetBool("isAiming", false);
        this.grunt.animator.SetBool("isMoving", true);
        this.grunt.animator.SetBool("isWalking", false);
        this.grunt.awareness.awareRadius = 8f;
        this.grunt.agent.speed = 10f;

        GameManager.ghostObjective = 0;
    }

    public override void SusDetected()
    {
        /*
        if (this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position, this.grunt.susPos,6))
        {
            this.grunt.aiMove_Utils.StopMovement(this.grunt.body, this.grunt.agent, this.grunt.animator);
        }
        else
        {
            this.grunt.agent.speed = 10f;
            this.grunt.aiMove_Utils.ResumeMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
            this.grunt.agent.SetDestination(this.grunt.susPos);
            this.grunt.animator.SetBool("isMoving", true);
            this.grunt.animator.SetBool("isWalking", false);
        }
        */
        if (this.grunt.CheckForPlayertInLineOfSight(45, 20))
        {
            this.grunt.SwitchState(this.grunt.aimingState);
        }
        if (this.grunt.awareness.susObject != null && this.grunt.susValue <= 50 )
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

    

    public override void UpdateState(GruntStateManager grunt)
    {
        RunEverySecond();
        if (grunt.susValue < 35)
        {
            grunt.SwitchState(grunt.suspiciousState);
        }
        else if (grunt.susPos != new Vector3(0,0,0))
        {
            GoToSus();
        }

    }

    void GoToSus()
    {
        if (grunt.CheckForPlayertInLineOfSight(60, 12))
        {
            this.grunt.SwitchState(this.grunt.aimingState);
        }
        if (this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position, this.grunt.susPos, 6))
        {
            this.grunt.aiMove_Utils.StopMovement(this.grunt.body, this.grunt.agent, this.grunt.animator);
        }
        else
        {
            this.grunt.agent.speed = 10f;
            this.grunt.aiMove_Utils.ResumeMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
            this.grunt.agent.SetDestination(this.grunt.susPos);
            this.grunt.animator.SetBool("isMoving", true);
            this.grunt.animator.SetBool("isWalking", false);
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