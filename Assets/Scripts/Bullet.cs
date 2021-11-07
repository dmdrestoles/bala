using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody bulletBody;

    void Start()
    {
        if (gameObject.name == "Bullet(Clone)" )
        {
            Destroy(gameObject, 0.3f);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Destroy(bulletBody);
    }
}
