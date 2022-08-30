using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockThrowStateManager : MonoBehaviour
{
    public RockThrowBaseState currentState;
    public LayerMask groundMask;
    public CapsuleCollider coll;
    //public string currentStateString;
    //public GameObject rock, origin;

    public RockThrowDistractingState distractingState = new RockThrowDistractingState();
    public RockThrowInertState inertState = new RockThrowInertState();
    public RockThrowThrownState thrownState = new RockThrowThrownState();

    void Start()
    {
        currentState = thrownState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        //Debug.Log("Debug: " + currentState.ToString());
    }

    public void SwitchState(RockThrowBaseState state)
    {
        currentState = state;
        //currentStateString = currentState.ToString();
        state.EnterState(this); 
        Debug.Log("Debug: " + currentState.ToString());
    }

}