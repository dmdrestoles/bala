using UnityEngine;
using UnityEngine.AI;


public class GruntPatrollingState : GruntBaseState
{
    GruntStateManager grunt;
    bool patrolStarted = false;
    float elapsed = 0;


    public override void EnterState(GruntStateManager stateManager)
    {
        patrolStarted = false;
        this.grunt = stateManager;
        this.grunt.awareness.awareRadius = 3f;
        this.grunt.aiMove_Utils.ResumeMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
    }

    public override void SusDetected(GruntStateManager stateManager)
    {
        if (this.grunt.awareness.susObject.name == "Muzzle")
        {
            this.grunt.susValue += 20;
            //this.grunt.SwitchState(this.grunt.huntingState);
        } 
        else if (this.grunt.awareness.susObject.name == "FootSteps")
        {
            this.grunt.susValue += 5;
            //this.grunt.SwitchState(this.grunt.suspiciousState);
        }

    }

    public override void UpdateState(GruntStateManager stateManager)
    {
        //Debug.Log(this.grunt.CheckForPlayertInLineOfSight(45, 20));
        if (this.grunt.CheckForPlayertInLineOfSight(45, 20))
        {
            this.grunt.susValue = 50;
            this.grunt.SwitchState(this.grunt.huntingState);
        }
        else if (this.grunt.susValue >= 20)
        {
            this.grunt.SwitchState(this.grunt.suspiciousState);
        }
        else if (this.grunt.susObject)
        {
            RunEverySecond();
        }
        Patrol(this.grunt.patrol1, this.grunt.patrol2);
    }

    void Patrol(Vector3 start, Vector3 end){
        this.grunt.animator.SetBool("isMoving", true);
        this.grunt.animator.SetBool("isWalking", true);
        this.grunt.agent.speed = 3.0f;
        if ((this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position, start,1)) && !this.grunt.agent.pathPending )
        {
            this.grunt.agent.SetDestination(end);
        }
        else if ((this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position,end,1)) && !this.grunt.agent.pathPending )
        {
            this.grunt.agent.SetDestination(start);
        }
        else if(!patrolStarted)
        {
            patrolStarted = true;
            this.grunt.agent.SetDestination(start);
        }
    }

    void RunEverySecond()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            SusDetected(this.grunt);
            Debug.Log("Debug: " + this.grunt.susValue);
        }
    }
}