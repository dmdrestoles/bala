using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM1State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.hallways[1].SetActive(true);
        Debug.Log("Debug: "+ "Hallway" + 2 + " is Active");
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway2Col")
        {
            stateManager.SwitchState(stateManager.h2State);
        }
    }
}
