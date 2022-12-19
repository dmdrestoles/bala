using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH11State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.hallways[8].SetActive(true);
        stateManager.hallways[9].SetActive(true);
        stateManager.hallways[10].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway10Col")
        {
            stateManager.SwitchState(stateManager.h10State);
        }
        else if (stateManager.collisionName == "Hallway9Col")
        {
            stateManager.SwitchState(stateManager.h9State);
        }
    }
}
