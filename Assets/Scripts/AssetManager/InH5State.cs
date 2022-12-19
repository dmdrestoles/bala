using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH5State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[1].SetActive(true);
        stateManager.modules[5].SetActive(true);
        stateManager.hallways[4].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module2Col")
        {
            stateManager.SwitchState(stateManager.m2State);
        }
        else if (stateManager.collisionName == "Module6Col")
        {
            stateManager.SwitchState(stateManager.m6State);
        }
    }
}
