using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class WeaponSelection : MonoBehaviour
{
    public GameObject[] weapons;
    public int selectedWeapon = 0;
    public TMP_Text weaponText;
    public Image weaponImage;

    void Start()
    {
        weapons[selectedWeapon].SetActive(true);
        weaponText.text = weapons[selectedWeapon].GetComponent<Weapon>().weaponName;
    }

    public void NextWeapon()
    {
        weapons[selectedWeapon].SetActive(false);
        selectedWeapon = (selectedWeapon + 1) % weapons.Length;
        weapons[selectedWeapon].SetActive(true);
        weaponText.text = weapons[selectedWeapon].GetComponent<Weapon>().weaponName;
    }

    public void PreviousWeapon()
    {
        weapons[selectedWeapon].SetActive(false);
        selectedWeapon--;
        if (selectedWeapon < 0)
        {
            selectedWeapon += weapons.Length;
        }
        weapons[selectedWeapon].SetActive(true);
        weaponText.text = weapons[selectedWeapon].GetComponent<Weapon>().weaponName;
    }
}
