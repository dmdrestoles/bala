using UnityEngine;

public class GruntRelaxedState : GruntBaseState
{
    Vector3 noPatrol = new Vector3(0,0,0);
    Vector3 originalPos;
    GruntStateManager grunt;
    float elapsed = 0;
    public override void EnterState(GruntStateManager stateManager)
    {
        this.grunt = stateManager;
        this.grunt.awareness.awareRadius = 5f;
        this.grunt.aiMove_Utils.StopMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
        this.originalPos = grunt.originalPos;
    }

    public override void SusDetected()
    {
        if (grunt.awareness.susObject.name == "Muzzle")
        {
            this.grunt.susValue +=20;
            //this.grunt.SwitchState(grunt.huntingState);
        } 
        else if (grunt.awareness.susObject.name == "FootSteps")
        {
            this.grunt.susValue +=5;
            //this.grunt.SwitchState(grunt.suspiciousState);
        }
        else if (grunt.awareness.susObject.name == "Rock")
        {
            this.grunt.susValue +=10;
            //this.grunt.SwitchState(grunt.suspiciousState);
        }
    }

    public override void UpdateState(GruntStateManager stateManager)
    {
        this.originalPos = grunt.originalPos;
        if (this.grunt.CheckForPlayerInLineOfSight(45, 10))
        {
            this.grunt.susValue = 50;
            this.grunt.SwitchState(this.grunt.huntingState);
        }
        else if (this.grunt.CheckForPlayerInLineOfSight(45, 20) && this.grunt.playerMoveStateManager.isVisible)
        {
            this.grunt.susValue = 50;
            this.grunt.SwitchState(this.grunt.huntingState);
        }
        else if (this.grunt.susValue >= 20)
        {
            this.grunt.isSomebodyThereAud.Play();
            this.grunt.SwitchState(this.grunt.suspiciousState);
        }
        else if (this.grunt.susObject)
        {
            RunEverySecond();
        }
        else if (this.grunt.patrol1 != noPatrol && this.grunt.patrol2 != noPatrol)
        {
            this.grunt.SwitchState(this.grunt.patrollingState);
        }
        else if ((this.grunt.aiMove_Utils.CheckDestinationReached(this.grunt.agent.transform.position, this.originalPos,1)))
        {
            //Debug.Log("Debug: "+grunt.gameObject.name + " Stopping Movement");
            this.grunt.aiMove_Utils.StopMovement(this.grunt.body,this.grunt.agent,this.grunt.animator);
        }
        else
        {
            this.grunt.agent.SetDestination(this.originalPos);
        }
    }

    void RunEverySecond()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f) {
            elapsed = elapsed % 1f;
            SusDetected();
            //Debug.Log("Debug: " + this.grunt.susValue);
        }
    }
}