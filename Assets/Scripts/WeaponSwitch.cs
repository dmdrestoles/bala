using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int equippedWeapon = 0;
    public bool isSwitching = false;
    public GameObject[] weapons;
    private GameObject weaponsList;

    void Start()
    {
        weaponsList = GameObject.FindWithTag("WeaponsList");
        SelectActiveWeapons();
        SelectWeapon();
    }

    void Update()
    {
        int previousEquippedWeapon = equippedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel")> 0f)
        {
            if (equippedWeapon >= weapons.Length)
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
                equippedWeapon = weapons.Length - 1;
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

        if (previousEquippedWeapon != equippedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectActiveWeapons()
    {
        for (int i = 0; i < weaponsList.transform.childCount - 1; i++)
        {
            if (weaponsList.transform.GetChild(i).GetComponent<GameObject>().activeSelf)
            {
                Debug.Log(weaponsList.transform.GetChild(i).name);
                weapons[i].GetComponent<Gun>().isActive = true;
            }
        }
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
