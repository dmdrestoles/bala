﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupLookingAtState : PickupBaseState
{
    public GameObject selectedObject;
    public int redCol, blueCol, greenCol;
    Material[] objectMatArr;
    bool flashingIn;

    public override void EnterState(PickupStateManager psm)
    {
        // reset highlight
        Debug.Log(psm.gameObject.name);
        flashingIn = true;
        objectMatArr = psm.gameObject.GetComponent<Renderer>().materials;
        foreach (Material mat in objectMatArr)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color32(0,0,0,0) );
        }
    }

    public override void UpdateState(PickupStateManager psm)
    {
        foreach (Material mat in objectMatArr)
        {
            //Debug.Log("FLASHING MATS");
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", new Color32(
                (byte)redCol,
                (byte)greenCol,
                (byte)blueCol,
                255) );
        }

        StartCoroutine(FlashObject());
        HandlePickup(psm);

        if (!psm.OnMouseOver())
        {
            // when not looking anymore
            foreach (Material mat in objectMatArr)
            {
                mat.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", new Color32(0,0,0,0) );
            }
            psm.Transition(psm.restState);
        }
    }

    public void HandlePickup(PickupStateManager psm)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Picked up object: " + gameObject.name);

            if (!psm.codex.activeSelf)
            {
                psm.codex.SetActive(true);
                psm.codexNotif.SetActive(true);
                psm.codexNotif.transform.GetChild(1).GetComponent<Image>().sprite = psm.sprite;
                psm.codexNotif.transform.GetChild(2).GetComponent<Text>().text = gameObject.name;
            }

            // Check if collectible, gun, etc.
            ChangeWeapon(psm);
            
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void ChangeWeapon(PickupStateManager psm)
    {
        // behavior on weapon change
        if (psm.isMelee)
        {
            Melee.isBoloAcquired = true;
            psm.meleeInv.transform.GetChild(1).gameObject.SetActive(true);
            psm.meleeInv.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        else if (psm.isPrimary)
        {
            psm.weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(gameObject.name);
            psm.weaponManager.isPickup = true;

            psm.primaryInv.transform.GetChild(0).GetComponent<Image>().sprite = psm.sprite;
        }
        else if (psm.isSecondary)
        {
            psm.weaponManager.secondary.GetComponent<ActiveWeaponManager>().SetNewWeapon(gameObject.name);
            psm.weaponManager.isPickup = true;

            psm.secondaryInv.transform.GetChild(0).GetComponent<Image>().sprite = psm.sprite;
        }
    }

    IEnumerator FlashObject(){
        yield return new WaitForSeconds(0.05f);
        if (flashingIn == true)
        {
            if (blueCol <= 0)
            {
                flashingIn = false;
            }
            else
            {
                blueCol -= 1;
                greenCol -= 1;
                redCol -= 1;
            }
        } else
        {
            if (blueCol >= 100)
            {
                flashingIn = true;
            }
            else
            {
                blueCol += 1;
                greenCol += 1;
                redCol += 1;
            }
        }
    }
}