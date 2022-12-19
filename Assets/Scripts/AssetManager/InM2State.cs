using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InM2State : AssetBaseState
{

    // Start is called before the first frame update
    public override void EnterState(AssetStateManager stateManager)
    {
        stateManager.modules[1].SetActive(true);
        stateManager.hallways[0].SetActive(true);
        stateManager.hallways[3].SetActive(true);
        stateManager.hallways[4].SetActive(true);
        stateManager.hallways[5].SetActive(true);
        stateManager.hallways[8].SetActive(true);   
    }

    // Update is called once per frame
    public override void UpdateState(AssetStateManager stateManager)
    {
        if (stateManager.collisionName == "Hallway1Col")
        {
            stateManager.SwitchState(stateManager.h1State);
        }
        else if(stateManager.collisionName == "Hallway4Col")
        {
            stateManager.SwitchState(stateManager.h4State);
        }
        else if(stateManager.collisionName == "Hallway5Col")
        {
            stateManager.SwitchState(stateManager.h5State);
        }
        else if(stateManager.collisionName == "Hallway6Col")
        {
            stateManager.SwitchState(stateManager.h6State);
        }
        else if(stateManager.collisionName == "Hallway9Col")
        {
            stateManager.SwitchState(stateManager.h9State);
        }
    }
}
