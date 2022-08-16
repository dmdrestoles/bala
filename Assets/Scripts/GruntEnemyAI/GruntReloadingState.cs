using UnityEngine;

public class GruntReloadingState : GruntBaseState
{
    float elapsed = 0f;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetBool("isReloading", true);
        grunt.aiMove_Utils.StopMovement(grunt.body,grunt.agent,grunt.animator);
        grunt.playReload();
    }

    public override void SusDetected()
    {
        
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 6f)
        {
            elapsed = elapsed % 6f;
            grunt.animator.SetBool("isReloading", false);
            grunt.currentAmmo = 5;
            grunt.SwitchState(grunt.aimingState);
        }       
    }
}