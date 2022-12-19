using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM6State : AssetBaseState
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
        if (stateManager.collisionName == "Hallway5Col")
        {
            stateManager.SwitchState(stateManager.h5State);
        }
    }
}
