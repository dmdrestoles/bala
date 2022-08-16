using UnityEngine;
using UnityEngine.AI;

public class AIMovement_Utils : MonoBehaviour
{
    public void StopMovement(Rigidbody r, NavMeshAgent agent, Animator animator){
        animator.SetBool("isMoving", false);
        r.freezeRotation = true;
        r.constraints = RigidbodyConstraints.FreezePosition;
        agent.isStopped = true;
        agent.velocity = Vector3.zero;
    }

    public void ResumeMovement(Rigidbody r, NavMeshAgent agent, Animator animator) {
        r.freezeRotation = false;
        r.constraints = RigidbodyConstraints.None;
        agent.isStopped = false;
        animator.SetBool("isMoving", true);
    }
    
    public bool CheckDestinationReached(Vector3 self, Vector3 target, float stopDistance) {
        float distanceToTarget = Vector3.Distance(self, target);
        bool result = false;
        if(distanceToTarget < stopDistance)
        {
            result = true;
        }
        Debug.Log("Debug: "+ "Distance to target is " + distanceToTarget);
        return result;
    }
}