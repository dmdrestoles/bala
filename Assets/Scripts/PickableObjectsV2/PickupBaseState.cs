using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickupBaseState : MonoBehaviour
{
    public abstract void EnterState(PickupStateManager psm);

    public abstract void UpdateState(PickupStateManager psm);
}
