using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH4State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.hallways[3].SetActive(true);
        stateManager.modules[1].SetActive(true);
        stateManager.modules[4].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module2Col")
        {
            stateManager.SwitchState(stateManager.m2State);
        }
        else if (stateManager.collisionName == "Module5Col")
        {
            stateManager.SwitchState(stateManager.m5State);
        }
    }
}
