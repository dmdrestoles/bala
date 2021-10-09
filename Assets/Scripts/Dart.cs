using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    public Rigidbody dartBody;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name == "Dart(Clone)" )
        {
            Destroy(gameObject, 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
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


