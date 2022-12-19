using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM4State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[3].SetActive(true);
        stateManager.hallways[2].SetActive(true);
        stateManager.hallways[7].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway3Col")
        {
            stateManager.SwitchState(stateManager.h3State);
        }
        else if(stateManager.collisionName == "Hallway8Col")
        {
            stateManager.SwitchState(stateManager.h8State);
        }
    }
}
