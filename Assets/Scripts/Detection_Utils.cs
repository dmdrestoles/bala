using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Utils : MonoBehaviour
{
    public bool IsHitWithinObjectAngle(RaycastHit hit, Transform source, float degree)
    {
        bool result = false;
        float deg = Vector3.Angle( source.forward, hit.transform.position - source.position );
        if(deg <= 45)
        {
            result = true;
        }
        return result;
    }

    public bool IsHitWithinObjectDistance(RaycastHit hit, float distance)
    {
        bool result = false;
        if (hit.distance <= distance)
        {
            result = true;
        }
        return result;
    }
}
