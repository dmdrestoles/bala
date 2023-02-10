using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilencioScript : MonoBehaviour
{
    float elapsed = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Text>().text != "")
        {
            StartCoroutine(RemoveDisplay());
        }
    }

    IEnumerator RemoveDisplay()
    {
        // Debug.Log("Start waiting");
        yield return new WaitForSeconds(3);
        this.GetComponent<Text>().text = "";
        // this.SetActive(false);
        // Debug.Log("End waiting");
    }
}
