using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLightDisable : MonoBehaviour
{
    public Light light;

    void OnPreCull()
    {
            light.enabled = false;
    }

    void OnPostRender()
    {
            light.enabled = true;
    }
}
