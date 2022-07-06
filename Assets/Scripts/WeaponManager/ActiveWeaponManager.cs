using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeaponManager : MonoBehaviour
{
    public GameObject[] weapons;

    [SerializeField] Gun activeWeapon;
    [SerializeField] AnimatorOverrideController controller;

    void Start()
    {
        activeWeapon = weapons[0].GetComponent<Gun>();
        controller = activeWeapon.GetController();
    }

    void Update()
    {
        
    }

    public AnimatorOverrideController GetController()
    {
        return controller;
    }
}
