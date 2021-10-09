using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class markerReached : MonoBehaviour
{

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            TutorialCutScene.reachedMarker = true;
        }
    }
}
