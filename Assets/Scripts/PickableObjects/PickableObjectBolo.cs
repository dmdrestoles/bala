using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectBolo : PickableObjectStateManager
{
    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            Destroy(gameObject);

            Melee.isBoloAcquired = true;

            if (Melee.isBoloAcquired){
                Debug.Log("Bolo now active!");
            }
        }
    }
}
