using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyGruntTest : MonoBehaviour
{
    NavMeshAgent agent;
    Rigidbody r;
    public GameObject firstPlayer;
    private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(firstPlayer.transform.position);
        animator.SetBool("isMoving", true);
        animator.SetBool("isWalking", true);
        Debug.Log(agent.remainingDistance);
    }

}
