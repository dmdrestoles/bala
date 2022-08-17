using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupRestState : PickupBaseState
{
    public override void EnterState(PickupStateManager psm)
    {
        // default on instantiate
    }

    public override void UpdateState(PickupStateManager psm)
    {
        if (psm.OnMouseOver())
        {
            psm.Transition(psm.lookAtState);
        }
    }
}
