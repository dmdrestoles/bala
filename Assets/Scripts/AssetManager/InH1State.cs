using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH1State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.hallways[0].SetActive(true);
        stateManager.hallways[1].SetActive(true);
        stateManager.hallways[2].SetActive(true);
        stateManager.modules[1].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway2Col")
        {
            stateManager.SwitchState(stateManager.h2State);
        }
        else if (stateManager.collisionName == "Module2Col")
        {
            stateManager.SwitchState(stateManager.m2State);
        }
    }
}
