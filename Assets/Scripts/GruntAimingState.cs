using UnityEngine;

public class GruntAimingState : GruntBaseState
{
    float elapsed = 0f;
    public override void EnterState(GruntStateManager grunt)
    {
        grunt.animator.SetBool("isAiming",true);
        grunt.aiMove_Utils.StopMovement(grunt.body, grunt.agent, grunt.animator);
    }

    public override void SusDetected(GruntStateManager grunt)
    {
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        Debug.Log(Vector3.Distance(grunt.transform.position, grunt.playerTransform.position));
        grunt.transform.LookAt(new Vector3(grunt.playerTransform.position.x, grunt.transform.position.y, grunt.playerTransform.position.z));
        if (Vector3.Distance(grunt.transform.position, grunt.playerTransform.position) > 20)
        {   
            grunt.SwitchState(grunt.huntingState);
        }
        else
        {
            HandleShooting(grunt);
        }
    }

    void HandleShooting(GruntStateManager grunt)
    {
        elapsed += Time.deltaTime;
        if (grunt.currentAmmo <= 0)
        {
            grunt.SwitchState(grunt.reloadingState);
        }
        else if (elapsed >= 5f) {
            elapsed = elapsed % 5f;
            grunt.SwitchState(grunt.firingState);
        }
    }
}