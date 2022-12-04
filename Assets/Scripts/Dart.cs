using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public Rigidbody dartBody;

    void Start()
    {
        if (gameObject.name == "Dart(Clone)" )
        {
            Destroy(gameObject, 2);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision");
            EnemyState enemyState = collision.gameObject.GetComponent<EnemyState>();
            enemyState.isAsleep = true;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

}


