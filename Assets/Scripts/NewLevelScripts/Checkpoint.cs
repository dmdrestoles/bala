using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNum;
    public GameObject barrier;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (checkpointNum == 1)
            {
                barrier.SetActive(false);
            }
            else if (checkpointNum == 2)
            {
                barrier.SetActive(false);
            }
        }
    }
}
