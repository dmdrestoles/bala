using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class CodexCollectibles : MonoBehaviour
{
    public GameObject collectible;
    public GameObject container;
    public GameObject otherContainer;
    public GameObject anotherContainer;
    public GameObject descInstructions;
    public GameObject descContainer;

    void Start()
    {
        descInstructions.SetActive(true);
    }

    public void showItems()
    {
        container.SetActive(true);
        descInstructions.GetComponent<TextMeshProUGUI>().text = "Please choose item to view";
    }

    public void closeOtherContainers()
    {
        otherContainer.SetActive(false);
        anotherContainer.SetActive(false);
        descContainer.SetActive(false);
        descInstructions.SetActive(true);
        descInstructions.GetComponent<TextMeshProUGUI>().text = "Please choose item to view";
    }
}

