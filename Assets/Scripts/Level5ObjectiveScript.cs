using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level5ObjectiveScript : MonoBehaviour
{
    public GameObject textHolder1;
    public GameObject objPanel;
    public GameObject objectives;

    // Start is called before the first frame update
    void Start()
    {
        objectives.GetComponent<Text>().text = "";
        StartCoroutine(StartLevelSequence());
    }

    IEnumerator StartLevelSequence()
    {
        GameManager.IsInputEnabled = true;
        objPanel.SetActive(true);
        objectives.SetActive(true);
        objectives.GetComponent<Text>().text = "Find the mission letter from one of the enemy camps.";

        yield return new WaitForSeconds(3.0f);

        objectives.GetComponent<Text>().text = ""; 
        objPanel.SetActive(false);
        objectives.SetActive(false);
    }
}
