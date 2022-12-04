using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickableObjectPaltik : PickableObjectStateManager
{
    public GameObject primary;
    public Sprite paltikSprite;

    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(objectName);
            weaponManager.isPickup = true;

            primary.transform.GetChild(0).GetComponent<Image>().sprite = paltikSprite;

            Destroy(gameObject);
        }
    }
}
