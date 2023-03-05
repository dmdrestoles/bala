﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummaryScript : MonoBehaviour
{

    List<GameObject> objectives;

    // Start is called before the first frame update
    void Start()
    {
        objectives = new List<GameObject>();
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            objectives.Add(gameObject.transform.GetChild(i).gameObject);
        }

        EnumerateObjectives();

        // Debug.Log(GameManager.mainObjective);
        // Debug.Log(GameManager.easterEggObjectives);
        // Debug.Log(GameManager.rifleObjective);
        // Debug.Log(GameManager.revolverObjective);
        // Debug.Log(GameManager.ghostObjective);
        // Debug.Log(GameManager.pacifistObjective);
        // Debug.Log(GameManager.killObjective);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateObjective(Text text, int value)
    {
        text.text = value.ToString();
    }

    void EnumerateObjectives()
    {
        for (int i = 0; i < objectives.Count; i++)
        {
            if (objectives[i].name == "Main Objective")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("isLetter4Found"));
            }
            if (objectives[i].name == "Easter Eggs")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("easterEggObjective"));
            }
            if (objectives[i].name == "M93")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("isM93Found"));
            }
            if (objectives[i].name == "Revolver")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("isRevolverFound"));
            }
            if (objectives[i].name == "No Detection")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("ghostObjective"));
            }
            if (objectives[i].name == "Pacifist")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("pacifistObjective"));
            }
            if (objectives[i].name == "Kill All")
            {
                UpdateObjective(objectives[i].transform.GetChild(0).GetComponent<Text>(), PlayerPrefs.GetInt("killObjective"));
            }
        }
    }
}
