using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InH8State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[3].SetActive(true);
        stateManager.modules[6].SetActive(true);
        stateManager.hallways[7].SetActive(true);
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Module4Col")
        {
            stateManager.SwitchState(stateManager.m4State);
        }
        else if (stateManager.collisionName == "Module7Col")
        {
            stateManager.SwitchState(stateManager.m7State);
        }
    }
}
