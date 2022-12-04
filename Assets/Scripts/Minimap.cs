using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public GameObject player;
    public GameObject camLight;
    Vector3 temp;

    void Update()
    {
        temp = player.transform.position;
        temp.y = 50f;
        transform.position = temp;
        camLight.transform.position = temp;
    }
}
