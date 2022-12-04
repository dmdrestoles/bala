using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeaponManager : MonoBehaviour
{
    public List<GameObject> weapons;

    [SerializeField] GameObject activeWeapon;
    [SerializeField] AnimatorOverrideController controller;

    void Start()
    {
        activeWeapon = weapons[0];
        controller = activeWeapon.GetComponent<Gun>().GetController();
    }

    void Update()
    {
    }

    public AnimatorOverrideController GetController()
    {
        return controller;
    }

    public void SetNewWeapon(string wep)
    {
        Debug.Log("Deactivating: " + activeWeapon);
        Vector3 spawnPoint = activeWeapon.GetComponent<Gun>().muzzleFlash.transform.position;
        Quaternion spawnRotation = activeWeapon.GetComponent<Gun>().gunInstance.transform.rotation;
        GameObject droppedWeapon = Instantiate(activeWeapon.GetComponent<Gun>().gunInstance, spawnPoint, spawnRotation);
        droppedWeapon.GetComponent<PickupStateManager>().isMain = false;
        droppedWeapon.name = activeWeapon.GetComponent<Gun>().weaponName;
        Debug.Log(gameObject.transform.position);
        droppedWeapon.SetActive(true);
        activeWeapon.GetComponent<Gun>().gunModelInHand.SetActive(false);
        activeWeapon.SetActive(false);

        foreach (GameObject weapon in weapons)
        {
            Debug.Log("Debug: "+ wep + " " + weapon.name + " " + weapon.name.Equals(wep));
            if (weapon.name.Equals(wep))
            {
                activeWeapon = weapon;
                break;
            }
        }
        activeWeapon.SetActive(true);
        activeWeapon.GetComponent<Gun>().gunModelInHand.SetActive(true);
        Debug.Log("Activating: " + activeWeapon.name);
        controller = activeWeapon.GetComponent<Gun>().GetController();
    }
}
