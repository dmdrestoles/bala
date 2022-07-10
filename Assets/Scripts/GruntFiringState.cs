using UnityEngine;

public class GruntFiringState : GruntBaseState
{
    float elapsed = 0f;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetTrigger("triggerFire");
        grunt.playFire();
    }

    public override void SusDetected(GruntStateManager grunt)
    {

    }

    public override void UpdateState(GruntStateManager grunt)
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 1f)
        {
            elapsed = elapsed % 1f;
            grunt.animator.ResetTrigger("triggerFire");
            grunt.currentAmmo -= 1;
            grunt.SwitchState(grunt.aimingState);
        }
    }
}