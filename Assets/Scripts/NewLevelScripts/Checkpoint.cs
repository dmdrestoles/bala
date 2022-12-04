using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNum;
    public GameObject barrier;
    public GameObject enemies;
    public GameObject nextLevelEnemies;

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (checkpointNum == 1)
            {
                barrier.SetActive(false);
                enemies.SetActive(true);
            }
            else if (checkpointNum == 2)
            {
                barrier.SetActive(false);
                enemies.SetActive(false);
                nextLevelEnemies.SetActive(true);
            }
        }
    }
}
