using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM7State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[6].SetActive(true);
        stateManager.hallways[5].SetActive(true);
        stateManager.hallways[6].SetActive(true);
        stateManager.hallways[7].SetActive(true);
        stateManager.hallways[9].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway6Col")
        {
            stateManager.SwitchState(stateManager.h6State);
        }
        else if (stateManager.collisionName == "Hallway7Col")
        {
            stateManager.SwitchState(stateManager.h7State);
        }
        else if (stateManager.collisionName == "Hallway8Col")
        {
            stateManager.SwitchState(stateManager.h8State);
        }
        else if (stateManager.collisionName == "Hallway10Col")
        {
            stateManager.SwitchState(stateManager.h10State);
        }
    }
}
