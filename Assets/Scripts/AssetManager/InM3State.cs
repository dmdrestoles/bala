using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM3State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[2].SetActive(true);
        stateManager.hallways[1].SetActive(true);
        stateManager.hallways[6].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway2Col")
        {
            stateManager.SwitchState(stateManager.h2State);
        }
        else if(stateManager.collisionName == "Hallway7Col")
        {
            stateManager.SwitchState(stateManager.h7State);
        }
    }
}
