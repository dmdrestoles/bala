using UnityEngine;

public class GruntAimingState : GruntBaseState
{
    float elapsed = 0f;
    public override void EnterState(GruntStateManager grunt)
    {
        elapsed = 0f;
        grunt.animator.SetBool("isAiming",true);
        grunt.muzzle.GetComponent<Collider>().enabled = false;
        grunt.aiMove_Utils.StopMovement(grunt.body, grunt.agent, grunt.animator);
        float rand = Random.Range(0.0f, 1.0f);
        if (rand <= 0.2f)
        {
            grunt.getHimAud.Play();
        }
    }

    public override void SusDetected()
    {
    }

    public override void UpdateState(GruntStateManager grunt)
    {
        //Debug.Log(Vector3.Distance(grunt.transform.position, grunt.playerTransform.position));
        grunt.transform.LookAt(new Vector3(grunt.playerTransform.position.x, grunt.transform.position.y, grunt.playerTransform.position.z));
        if (Vector3.Distance(grunt.transform.position, grunt.playerTransform.position) > 25 || grunt.playerTransform.position == null)
        {   
            grunt.SwitchState(grunt.huntingState);
        }
        else
        {
            grunt.aiMove_Utils.StopMovement(grunt.body, grunt.agent, grunt.animator);
            AddSusLevel(grunt);
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
        else if (elapsed >= 1f) {
            elapsed = elapsed % 4f;
            grunt.SwitchState(grunt.firingState);
        }
    }

    void AddSusLevel(GruntStateManager grunt)
    {
        if (grunt.susValue <= 50)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 2f) {
                elapsed = elapsed % 2f;
                grunt.susValue += 1;
                Debug.Log("Debug: " + grunt.susValue);
            }
        }
    }
}