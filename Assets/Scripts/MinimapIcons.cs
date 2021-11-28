using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcons : MonoBehaviour
{
    public GameObject objective;
    public GameObject minimapCamera;
    Vector3 temp;

    void Update()
    {
        temp = objective.transform.position;
        transform.position = temp;
        transform.rotation = minimapCamera.transform.rotation;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minimapCamera.transform.position.x - 13.5f, 13.5f + minimapCamera.transform.position.x),
            transform.position.y + 5f,
            Mathf.Clamp(transform.position.z, minimapCamera.transform.position.z - 14f, 14f + minimapCamera.transform.position.z)
            );
    }
}
