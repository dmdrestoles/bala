using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsList : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
