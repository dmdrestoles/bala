using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH10State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.hallways[8].SetActive(true);
        stateManager.hallways[9].SetActive(true);
        stateManager.hallways[10].SetActive(true);
        stateManager.modules[6].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module2Col")
        {
            stateManager.SwitchState(stateManager.m2State);
        }
        else if (stateManager.collisionName == "Hallway8Col")
        {
            stateManager.SwitchState(stateManager.h8State);
        }
        else if (stateManager.collisionName == "Hallway9Col")
        {
            stateManager.SwitchState(stateManager.h9State);
        }
        else if (stateManager.collisionName == "Hallway11Col")
        {
            stateManager.SwitchState(stateManager.h11State);
        }
    }
}
