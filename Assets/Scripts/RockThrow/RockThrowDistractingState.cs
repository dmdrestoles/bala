using System.Collections;
using UnityEngine;

public class RockThrowDistractingState : RockThrowBaseState
{
    float elapsed = 0f;
    public override void EnterState(RockThrowStateManager sm)
    {
        elapsed = 0f;
        sm.coll.radius = 10.0f;
        sm.rockThrowSFX.Play();
    }

    public override void UpdateState(RockThrowStateManager sm)
    {
        if (!Physics.CheckSphere(sm.transform.position, 0.5f, sm.groundMask))
        {
            sm.SwitchState(sm.thrownState);
        }
        TurnInert(sm);
    }

    void TurnInert(RockThrowStateManager sm)
    {
        elapsed += Time.deltaTime;
        Debug.Log(elapsed);
        if (elapsed >= 5f) {
            elapsed = elapsed % 5f;
            sm.SwitchState(sm.inertState);
        }
    }
}