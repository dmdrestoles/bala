using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int equippedWeapon = 0;

    void Start()
    {
        SelectWeapon();
    }

    void Update()
    {
        int previousEquippedWeapon = equippedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel")> 0f)
        {
            if (equippedWeapon >= transform.childCount - 1)
            {
                equippedWeapon = 0;
            }
            else
            {
                equippedWeapon += 1;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (equippedWeapon <= 0)
            {
                equippedWeapon = transform.childCount - 1;
            }
            else
            {
                equippedWeapon -= 1;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equippedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2) 
        {
            equippedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            equippedWeapon = 2;
        }

        if (previousEquippedWeapon != equippedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon()
    {
        int i = 0;

        foreach (Transform weapon in transform)
        {
            if (i == equippedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }

            i += 1;
        }
    }
}
