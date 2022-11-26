using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles Input for Throwing the rock
public class RockThrowing : MonoBehaviour
{
    public GameObject rock, rockOrigin, fpsCamera;
    public PlayerMoveStateManager pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G))
        {
            ThrowRock();
        }
    }

    void ThrowRock()
    {
        GameObject rockForward;

        rockForward = Instantiate(rock, rockOrigin.transform.position, rockOrigin.transform.rotation);
        rockForward.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 25 ); 
    }
}
