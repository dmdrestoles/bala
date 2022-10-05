using System.Collections;
using UnityEngine;

public class RockThrowThrownState : RockThrowBaseState
{
    public override void EnterState(RockThrowStateManager sm)
    {
        sm.coll.radius = 0;
    }

    public override void UpdateState(RockThrowStateManager sm)
    {
        
        if (Physics.CheckSphere(sm.transform.position, 0.5f, sm.groundMask))
        {
            sm.SwitchState(sm.distractingState);
        }
    }
}