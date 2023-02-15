using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SilencioScript : MonoBehaviour
{
    float elapsed = 0.0f;
    public static GameObject objPanel;
    // Start is called before the first frame update
    void Start()
    {
        objPanel = this.transform.parent.Find("objPanel").gameObject;
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
        objPanel.SetActive(false);
        // this.SetActive(false);
        // Debug.Log("End waiting");
    }
}
