using System.Collections;
using UnityEngine;

public class GruntDeathState : GruntBaseState
{
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetTrigger("triggerDeath");
        grunt.aiMove_Utils.StopMovement(grunt.body,grunt.agent,grunt.animator);
    }

    public override void UpdateState(GruntStateManager grunt)
    {
    }

    public override void SusDetected()
    {

    }
}