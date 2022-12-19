using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH2State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        for (int i = 0; i < 3; i++)
        {
            stateManager.hallways[i].SetActive(true);
            Debug.Log("Debug: "+ "Hallway" + i + 1 + " is Active");
        }
        stateManager.modules[2].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway1Col")
        {
            stateManager.SwitchState(stateManager.h1State);
        }
        else if (stateManager.collisionName == "Hallway3Col")
        {
            stateManager.SwitchState(stateManager.h3State);
        }
        else if (stateManager.collisionName == "Module3Col")
        {
            stateManager.SwitchState(stateManager.m3State);
        }
    }
}
