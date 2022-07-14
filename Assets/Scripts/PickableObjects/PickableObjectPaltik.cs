using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObjectPaltik : PickableObjectStateManager
{
    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(objectName);
            weaponManager.isPickup = true;
            Destroy(gameObject);
        }
    }
}
