using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int equippedWeapon = 0;

    private int primaryWep;
    private int secondaryWep;
    private int melee;

    public bool isSwitching = false;

    public GameObject[] weapons;
    public Animator animator;

    void Start()
    {
        primaryWep = PlayerPrefs.GetInt("Primary");
        secondaryWep = PlayerPrefs.GetInt("Secondary");
        melee = PlayerPrefs.GetInt("Melee");
        // LoadWeapons();
        SelectActiveWeapons();
        SelectWeapon();
    }

    void Update()
    {
        int previousEquippedWeapon = equippedWeapon;

        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(MeleeAttack());
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

    void SelectActiveWeapons()
    {
        Debug.Log(weapons[primaryWep].GetComponent<Gun>().weaponName);
        weapons[primaryWep].GetComponent<Gun>().isActive = true;
        Debug.Log(weapons[secondaryWep].GetComponent<Gun>().weaponName);
        weapons[secondaryWep].GetComponent<Gun>().isActive = true;
        Debug.Log(weapons[melee].GetComponent<Melee>().weaponName);
        weapons[melee].GetComponent<Melee>().isActive = true;

        equippedWeapon = primaryWep;
    }

    void SelectWeapon()
    {
        int i = 0;
        isSwitching = true;

        foreach (Transform weapon in transform)
        {
            if (i == equippedWeapon && weapon.GetComponent<Gun>().isActive )
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

    IEnumerator MeleeAttack()
    {
        weapons[equippedWeapon].SetActive(false);
        yield return new WaitForSeconds(0.25f);
        weapons[melee].SetActive(true);
        animator.SetTrigger("Melee");
        yield return new WaitForSeconds(0.25f);
        weapons[melee].GetComponent<Melee>().CheckForEnemies();
        yield return new WaitForSeconds(0.25f);
        weapons[melee].gameObject.SetActive(false);
        weapons[equippedWeapon].SetActive(true);
    }

    public int GetPrimaryWeapon()
    {
        return primaryWep;
    }

    public int GetSecondaryWeapon()
    {
        return secondaryWep;
    }

    public void SetPrimary()
    {
        primaryWep = PlayerPrefs.GetInt("Primary");
    }

    public void SetSecondary()
    {
        secondaryWep = PlayerPrefs.GetInt("Secondary");
        SelectActiveWeapons();
    }

}
