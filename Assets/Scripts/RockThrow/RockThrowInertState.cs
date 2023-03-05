using System.Collections;
using UnityEngine;

public class RockThrowInertState : RockThrowBaseState
{
    public override void EnterState(RockThrowStateManager sm)
    {
        sm.coll.radius = 0;
        Debug.Log("Rock turned into inert.");
    }

    public override void UpdateState(RockThrowStateManager sm)
    {
        sm.Destroy();
    }

}