using System.Collections;
using UnityEngine;

public class GruntDeathState : GruntBaseState
{
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.ResetTrigger("triggerDeath");
        grunt.animator.SetTrigger("triggerDeath");
        grunt.aiMove_Utils.StopMovement(grunt.body,grunt.agent,grunt.animator);
        grunt.awareIndi.SetActive(false);
        PlayerPrefs.SetInt("pacifistObjective",0);
        PlayerPrefs.SetInt("killObjective", PlayerPrefs.GetInt("killObjective") + 1);
    }

    public override void UpdateState(GruntStateManager grunt)
    {
    }

    public override void SusDetected()
    {

    }
}