﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectWorld : PickableObjectBaseState
{
    public GameObject selectedObject;
    public int redCol, blueCol, greenCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    
    MouseLook mouseLook;
    string objectName;
    PickableObjectStateManager stateManager;

    public override void EnterState(PickableObjectStateManager obj){
        //Debug.Log("Hello from the world state!");
        stateManager = obj;
        mouseLook = obj.mouseLook;
        objectName = obj.objectName;
    }

    public override void UpdateState(PickableObjectStateManager obj){
        if (lookingAtObject && objectName == mouseLook.GetSelectedObject())
        {
            Material[] objectMatArr = selectedObject.GetComponent<Renderer>().materials;

            foreach (Material mat in objectMatArr)
            {
                //Debug.Log("FLASHING MATS");
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", new Color32(
                    (byte)redCol,
                    (byte)greenCol,
                    (byte)blueCol,255) );
            }
        }
    }

    public override void TransitionState(PickableObjectStateManager obj){
        
    }

    public void OnMouseOver(){
        //Debug.Log(mouseLook.ToString());
        selectedObject = GameObject.Find(mouseLook.GetSelectedObject());
        //Debug.Log("Debug: Looking at " + selectedObject.name);
        if (objectName == mouseLook.GetSelectedObject())
        {
            //Debug.Log("Pickupable!");
            lookingAtObject = true;
            stateManager.HandleObjectPickup(objectName);
            if(!startedFlashing)
            {
                startedFlashing = true;
                StartCoroutine(FlashObject());
            }
        }
    }

    public void OnMouseExit(){
        
        startedFlashing = false;
        lookingAtObject = false;
        StopCoroutine(FlashObject());
        if (selectedObject != null){
            Material[] objectMatArr = selectedObject.GetComponent<Renderer>().materials;

            foreach (Material mat in objectMatArr)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", new Color32(0,0,0,0) );
            }
        }
    }

    IEnumerator FlashObject(){
        while(lookingAtObject == true)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn == true)
            {
                if (blueCol <= 0)
                {
                    flashingIn = false;
                }
                else
                {
                    blueCol -= 4;
                    greenCol -= 4;
                    redCol -= 4;
                }
            } else
            {
                if (blueCol >= 100)
                {
                    flashingIn = true;
                }
                else
                {
                    blueCol += 4;
                    greenCol += 4;
                    redCol += 4;
                }
            }
        }
    }
}
