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

    public override void SusDetected()
    {

    }

    public override void UpdateState(GruntStateManager grunt)
    {
        grunt.transform.LookAt(new Vector3(grunt.playerTransform.position.x, grunt.transform.position.y, grunt.playerTransform.position.z));
        elapsed += Time.deltaTime;
        if (elapsed >= 4f)
        {
            elapsed = elapsed % 4f;
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
            grunt.playerState.TakeDamage(30);
        }
    }

    Vector3 CalculateMiss(Vector3 target)
    {
        float distance = Vector3.Distance(grunt.transform.position, grunt.playerTransform.position);
        double missFactor = System.Math.Pow(2, 0.01*(double)distance) - 1;
        float a = (float)missFactor;

        float missChanceFactor = 2.0f;
        a *= missChanceFactor;

        float xRand = Random.Range(-a, a);
        float yRand = Random.Range(-a, a);
        float zRand = Random.Range(-a, a);

        target.x += xRand;
        target.y += yRand;
        target.z += zRand;

        return target;
    }
}