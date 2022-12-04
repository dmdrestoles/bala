using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruntAwareness : MonoBehaviour
{
    public GameObject susObject;
    public float awareRadius;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<CapsuleCollider>().radius = awareRadius;
    }

    void OnTriggerStay(Collider other)
    {
     if (other.gameObject.tag == "SusObject")
        {
            susObject = other.gameObject;
            //Debug.Log(susObject.name);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SusObject"  || other == null)
        {
            StartCoroutine(HandleSusObjectDecay());
        }
    }

    IEnumerator HandleSusObjectDecay()
    {
        yield return new WaitForSeconds(0);
        susObject = null;
    }
}
