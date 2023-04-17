using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int enemyKilled = 0;
    public int enemyCount = 0;
    int originalEnemyCount = 0;
    List<GruntStateManager> enemyList;

    void Start()
    {

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            Transform child = gameObject.transform.GetChild(i);
            if (child.name == "Guardia Civil")
            {
                enemyCount++;
                originalEnemyCount++;
            }
        }

        //Debug.Log("The number of child objects named " + "Guardia Civil" + " is: " + enemyCount);

    }

    // Update is called once per frame
    void Update()
    {
        enemyKilled = originalEnemyCount - enemyCount;
    }
}
