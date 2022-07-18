using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickableObjectRemington : PickableObjectStateManager
{
    public GameObject primary;
    public GameObject codexNotif;
    public GameObject codex;
    public Sprite remingtonSprite;

    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(objectName);
            weaponManager.isPickup = true;

            primary.transform.GetChild(0).GetComponent<Image>().sprite = remingtonSprite;

            if (!codex.active)
            {
                codex.SetActive(true);
                codexNotif.SetActive(true);
                codexNotif.transform.GetChild(1).GetComponent<Image>().sprite = remingtonSprite;
                codexNotif.transform.GetChild(2).GetComponent<Text>().text = objectName;
            }

            Destroy(gameObject);
        }
    }
}
