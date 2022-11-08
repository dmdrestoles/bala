using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNum;
    public GameObject barrier;
    public GameObject player;
    public GameObject door;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            barrier.SetActive(false);
            if (player.transform.position.x > 22)
            {
                Debug.Log("Debug: Door Closed");
                door.SetActive(true);
                //barrier.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (player.transform.position.x > 22)
        {
            Debug.Log("Debug: Door Closed");
            door.SetActive(true);
            //barrier.SetActive(true);
        }
    }
}
