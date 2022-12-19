using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH7State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[2].SetActive(true);
        stateManager.modules[6].SetActive(true);
        stateManager.hallways[6].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module3Col")
        {
            stateManager.SwitchState(stateManager.m3State);
        }
        else if (stateManager.collisionName == "Module7Col")
        {
            stateManager.SwitchState(stateManager.m7State);
        }
    }
}
