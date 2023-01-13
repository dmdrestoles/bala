using System.Collections;
using UnityEngine;

public class GruntDeathState : GruntBaseState
{
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetTrigger("triggerDeath");
        grunt.aiMove_Utils.StopMovement(grunt.body,grunt.agent,grunt.animator);
        GameManager.pacifistObjective = 0;
        Debug.Log("Pacifist: " + GameManager.pacifistObjective);
        GameManager.killObjective += 1;
    }

    public override void UpdateState(GruntStateManager grunt)
    {
    }

    public override void SusDetected()
    {

    }
}