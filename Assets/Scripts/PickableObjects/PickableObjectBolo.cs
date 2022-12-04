using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickableObjectBolo : PickableObjectStateManager
{
    public GameObject melee;
    public GameObject codexNotif;
    public GameObject codex;
    public Sprite boloSprite;

    public override void HandleObjectPickup(string objectName){
        if (Input.GetKeyDown(KeyCode.F)){
            Debug.Log("Picked up object: " + objectName);
            gameObject.SetActive(false);
            Destroy(gameObject);

            Melee.isBoloAcquired = true;

            if (Melee.isBoloAcquired){
                Debug.Log("Bolo now active!");

                melee.transform.GetChild(1).gameObject.SetActive(true);
                melee.transform.GetChild(0).gameObject.SetActive(false);

                if (!codex.activeSelf)
                {
                    codex.SetActive(true);
                    codexNotif.SetActive(true);
                    codexNotif.transform.GetChild(1).GetComponent<Image>().sprite = boloSprite;
                    codexNotif.transform.GetChild(2).GetComponent<Text>().text = objectName;
                }
            }
        }
    }
}
