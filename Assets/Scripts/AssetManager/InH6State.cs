using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH6State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[1].SetActive(true);
        stateManager.modules[6].SetActive(true);
        stateManager.hallways[5].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module2Col")
        {
            stateManager.SwitchState(stateManager.m2State);
        }
        else if (stateManager.collisionName == "Module7Col")
        {
            stateManager.SwitchState(stateManager.m7State);
        }
    }
}
