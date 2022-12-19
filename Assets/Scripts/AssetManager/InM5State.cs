using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM5State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[4].SetActive(true);
        stateManager.hallways[3].SetActive(true);
        Debug.Log("Debug: "+ "Hallway" + 2 + " is Active");
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway4Col")
        {
            stateManager.SwitchState(stateManager.h4State);
        }
    }
}
