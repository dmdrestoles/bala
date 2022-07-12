using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntFiringState : GruntBaseState
{
    float elapsed = 0f;
    RaycastHit hit;
    GameObject bulletForward;
    GruntStateManager grunt;
    public override void EnterState(GruntStateManager grunt)
    {
        this.grunt = grunt;
        this.grunt.muzzle.GetComponent<Collider>().enabled = true;
        this.Shoot();
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

    void Shoot()
    {
        grunt.animator.SetTrigger("triggerFire");
        grunt.playFire();

        bool shootCast = Physics.Linecast(grunt.muzzle.transform.position, CalculateMiss(grunt.playerTransform.position), out hit);
        bulletForward = grunt.SpawnBullet();
        bulletForward.GetComponent<Rigidbody>().velocity = grunt.transform.TransformDirection(Vector3.forward * 500);
        
        if (shootCast && hit.transform.tag == "Player")
        {
            Debug.DrawLine(grunt.muzzle.transform.position, hit.point, Color.black, 2f);
            PlayerState player = hit.transform.GetComponent<PlayerState>();
            player.TakeDamage(25);
        }
    }

    Vector3 CalculateMiss(Vector3 target)
    {
        float distance = Vector3.Distance(grunt.transform.position, grunt.playerTransform.position);
        double missFactor = System.Math.Pow(2, 0.01*(double)distance) - 1;
        float a = (float)missFactor;

        float xRand = Random.Range(-a, a);
        float yRand = Random.Range(-a, a);
        float zRand = Random.Range(-a, a);

        target.x += xRand;
        target.y += yRand;
        target.z += zRand;

        return target;
    }
}