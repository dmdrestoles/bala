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
        activeWeapon.SetActive(false);

        foreach (GameObject weapon in weapons)
        {
            Debug.Log(weapon.name);
            if (weapon.name.Equals(wep))
            {
                activeWeapon = weapon;
                break;
            }
        }
        Debug.Log("Activating: " + activeWeapon.name);
        activeWeapon.SetActive(true);
        controller = activeWeapon.GetComponent<Gun>().GetController();
    }
}
