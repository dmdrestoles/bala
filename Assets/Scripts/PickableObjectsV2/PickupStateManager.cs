﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;                      

public class PickupStateManager : MonoBehaviour
{
    public PickupBaseState currentState;
    public PickupDebugState debugState;
    public PickupLookingAtState lookAtState;
    public PickupRestState restState;

    public bool isMain, isPrimary, isSecondary, isMelee;

    public MouseLook mouseLook;
    public string objectName;

    public GameObject codexNotif;
    public GameObject codex;
    public Sprite sprite;

    public WeaponManager weaponManager;
    public GameObject primaryInv, secondaryInv, meleeInv;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<PickupDebugState>();
        gameObject.AddComponent<PickupLookingAtState>();
        gameObject.AddComponent<PickupRestState>();

        debugState = gameObject.GetComponent<PickupDebugState>();
        lookAtState = gameObject.GetComponent<PickupLookingAtState>();
        restState = gameObject.GetComponent<PickupRestState>();

        if (isMain)
        {
            currentState = debugState;
        }
        else
        {
            currentState = restState;
        }

        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void Transition(PickupBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public bool OnMouseOver(){
        return mouseLook.GetSelectedObject() == gameObject.name;
    }

    /*
    public virtual void HandleObjectPickup(string objectName)
    {
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    */
}