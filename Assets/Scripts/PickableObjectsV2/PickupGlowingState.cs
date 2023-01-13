using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupGlowingState : PickupBaseState
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
        if (psm.OnMouseOver())
        {
            HandlePickup(psm);
        }

        if (!psm.IsPlayerNear())
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

    void HandlePickup(PickupStateManager psm)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Picked up object: " + gameObject.name);
            Debug.Log("Debug: codex notif active " + !psm.codex.activeSelf);
            HandlePreferences(gameObject.name);
            psm.pickupSFX.Play();
            if (!psm.codex.activeSelf)
            {
                psm.codex.SetActive(true);
                psm.codexNotif.SetActive(true);
                //Debug.Log("Debug: codex notif active " );
                psm.codexNotif.transform.GetChild(1).GetComponent<Image>().sprite = psm.sprite;
                psm.codexNotif.transform.GetChild(2).GetComponent<Text>().text = gameObject.name;
            }

            if (gameObject.GetComponent<Objective>() != null)
            {
                string objectiveName = gameObject.GetComponent<Objective>().name;

                GameManager.UpdateObjective(objectiveName);
            }

            // Check if collectible, gun, etc.
            ChangeWeapon(psm);
            HandleAmmo(psm);
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    void HandlePreferences(string name)
    {
        if (name.Contains("Letter-1"))
        {
            PlayerPrefs.SetInt("isLetter1Found", 1);
        } 
        else if (name.Contains("Letter-2"))
        {
            PlayerPrefs.SetInt("isLetter2Found", 1);
        }
        else if (name.Contains("Letter-3"))
        {
            PlayerPrefs.SetInt("isLetter3Found", 1);
        }
        else if (name.Contains("Letter-4"))
        {
            PlayerPrefs.SetInt("isLetter4Found", 1);
        }
        else if (name.Contains("Letter-5"))
        {
            PlayerPrefs.SetInt("isLetter5Found", 1);
        }
        else if (name.Contains("Cross"))
        {
            PlayerPrefs.SetInt("isCrossFound", 1);
        }
        else if (name.Contains("Cedula"))
        {
            PlayerPrefs.SetInt("isCedulaFound", 1);
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
            PlayerPrefs.SetInt("isBoloFound", 1);
            //Debug.Log("Debug: Is Bolo Acquired " + Melee.isBoloAcquired);
            return;
        }

        else if (psm.isPrimary)
        {
            psm.weaponManager.primary.GetComponent<ActiveWeaponManager>().SetNewWeapon(gameObject.name);
            psm.weaponManager.isPickup = true;
            psm.weaponManager.isPrimary = true;
            if (gameObject.name == "M93")
            {
                PlayerPrefs.SetInt("isM93Found", 1);
            }
            else
            {
                PlayerPrefs.SetInt("isPaltikFound",1);
            }
            Debug.Log("Debug: " + gameObject.name + " acquired");
            //psm.primaryInv.transform.GetChild(0).GetComponent<Image>().sprite = psm.sprite;
        }
        else if (psm.isSecondary)
        {
            psm.weaponManager.secondary.GetComponent<ActiveWeaponManager>().SetNewWeapon(gameObject.name);
            psm.weaponManager.isPickup = true;
            psm.weaponManager.isPrimary = false;
            PlayerPrefs.SetInt("isRevolverFound", 1);
            //psm.secondaryInv.transform.GetChild(0).GetComponent<Image>().sprite = psm.sprite;
        }
    }

    void HandleAmmo(PickupStateManager psm)
    {
        if (psm.objectName== "Ammunition")
        {
            PlayerPrefs.SetInt("isAmmunitionFound", 1);
            if (psm.weaponManager.primary.activeSelf)
            {
                //Debug.Log("Debug: Get Max Ammo: "+psm.weaponManager.primary.GetComponent<ActiveWeaponManager>().activeWeapon.GetComponent<Gun>().maxAmmo);
                psm.weaponManager.primary.GetComponent<ActiveWeaponManager>().activeWeapon.GetComponent<Gun>().maxAmmo += 5;
            }
            if (psm.weaponManager.secondary.activeSelf)
            {
                //Debug.Log("Debug: Get Max Ammo: "+psm.weaponManager.secondary.GetComponent<ActiveWeaponManager>().activeWeapon.GetComponent<Gun>().maxAmmo  + " HERE");
                psm.weaponManager.secondary.GetComponent<ActiveWeaponManager>().activeWeapon.GetComponent<Gun>().maxAmmo += 5;
            }
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
                blueCol -= 5;
                greenCol -= 5;
                redCol -= 5;
            }
        } else
        {
            if (blueCol >= 100)
            {
                flashingIn = true;
            }
            else
            {
                blueCol += 5;
                greenCol += 5;
                redCol += 5;
            }
        }
    }
    

}
