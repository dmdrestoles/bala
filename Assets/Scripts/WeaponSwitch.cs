using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int equippedWeapon = 0;

    private int primaryWep;
    private int secondaryWep;

    public bool isSwitching = false;
    public GameObject[] weapons;

    void Start()
    {
        primaryWep = PlayerPrefs.GetInt("Primary");
        secondaryWep = PlayerPrefs.GetInt("Secondary");
        // LoadWeapons();
        SelectActiveWeapons();
        SelectWeapon();
    }

    void Update()
    {
        int previousEquippedWeapon = equippedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            while (true)
            {
                equippedWeapon -= 1;

                if (equippedWeapon < 0)
                {
                    equippedWeapon = weapons.Length - 1;
                }
                
                if (weapons[equippedWeapon].GetComponent<Gun>().isActive)
                {
                    break;
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            while (true)
            {
                equippedWeapon += 1;

                if (equippedWeapon >= weapons.Length)
                {
                    equippedWeapon = 0;
                }
                
                if (weapons[equippedWeapon].GetComponent<Gun>().isActive)
                {
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = primaryWep;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) 
        {
            equippedWeapon = secondaryWep;
        }

        if (previousEquippedWeapon != equippedWeapon)
        {
            SelectWeapon();
        }
    }

    // void LoadWeapons()
    // {
    //     foreach (Transform weapon in transform)
    //     {
    //         weapons.Add(weapon.gameObject);
    //         Debug.Log("Adding weapon: " + weapon.gameObject.GetComponent<Gun>().weaponName);
    //     }
    // }

    void SelectActiveWeapons()
    {
        Debug.Log(weapons[primaryWep].GetComponent<Gun>().weaponName);
        weapons[primaryWep].GetComponent<Gun>().isActive = true;
        Debug.Log(weapons[secondaryWep].GetComponent<Gun>().weaponName);
        weapons[secondaryWep].GetComponent<Gun>().isActive = true;

        equippedWeapon = primaryWep;
    }

    void SelectWeapon()
    {
        int i = 0;
        isSwitching = true;

        foreach (Transform weapon in transform)
        {
            if (i == equippedWeapon && weapon.GetComponent<Gun>().isActive)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i += 1;
        }

        isSwitching = false;
    }
}
