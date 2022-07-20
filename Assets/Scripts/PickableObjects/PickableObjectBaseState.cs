using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PickableObjectBaseState : MonoBehaviour
{
    public abstract void EnterState(PickableObjectStateManager obj);

    public abstract void UpdateState(PickableObjectStateManager obj);
}
