using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectRemington : PickableObjectStateManager
{
    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(objectName);
            weaponManager.isPickup = true;
            Destroy(gameObject);

            if (Melee.isBoloAcquired){
                Debug.Log("Bolo now active!");
            }
        }
    }
}
