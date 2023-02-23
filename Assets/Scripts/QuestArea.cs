﻿using UnityEngine;
using System.Linq;
using System.Dynamic;

public class QuestArea : MonoBehaviour
{
    public GameObject questMarker;
    public GameObject minimapHighlight;
    public GameObject searchAreaMarker;
    public GameObject collectible;

    static bool mainObjDone = false;

    void Update()
    {
        UpdateMinimap();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player" && this.transform.parent.Find("SearchAreaMarker").gameObject.active)
        {
            minimapHighlight.SetActive(true);
            questMarker.SetActive(false);
            // Debug.Log("Player inside Search Area");
        }
    }
    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == "Player" && this.transform.parent.Find("SearchAreaMarker").gameObject.active)
        {
            minimapHighlight.SetActive(true);
            questMarker.SetActive(false);
            // Debug.Log("Player still inside of Search Area");
        }
        else if (col.gameObject.name == "Player" && !this.transform.parent.Find("SearchAreaMarker").gameObject.active)
        {
            minimapHighlight.SetActive(false);
            //Debug.Log("Player still inside of Search Area but letter is collected");
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player" && this.transform.parent.Find("SearchAreaMarker").gameObject.active)
        {
            minimapHighlight.SetActive(false);
            questMarker.SetActive(true);
            // Debug.Log("Player went out");
        }
    }

    void UpdateMinimap()
    {
        var name = "";
        if (this.transform.parent.name == "Search Area7.1")
        {
            if (collectible.transform.childCount == 1)
            {
                name = collectible.transform.GetChild(0).name;
            }
            else if (collectible.transform.childCount == 0)
            {
                name = "none";
            }

            if (name == "Letter-4")
            {
                // Debug.Log("Disable Search Area 7.1");
                this.transform.parent.Find("SearchAreaMarker").gameObject.SetActive(false);
                this.transform.parent.Find("QuestMarker").gameObject.SetActive(false);
            }
        }
        else if (this.transform.parent.name == "Search Area7.4" && !mainObjDone)
        {
            if (collectible.transform.childCount == 1)
            {
                name = collectible.transform.GetChild(0).name;
            }
            else if (collectible.transform.childCount == 0)
            {
                name = "none";
            }

            if (name == "Letter-1" || name == "none")
            {
                var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "SearchAreaMarker");
                var objects1 = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "QuestMarker");

                foreach (GameObject obj in objects)
                {
                    obj.SetActive(false);
                }
                foreach (GameObject obj in objects1)
                {
                    obj.SetActive(false);
                }

                // Debug.Log("Disable Search Area 7.4");
                this.transform.parent.Find("SearchAreaMarker").gameObject.SetActive(false);
                this.transform.parent.Find("QuestMarker").gameObject.SetActive(false);

                mainObjDone = true;
            }
        }
        else if (collectible.transform.childCount == 0)
        {
            searchAreaMarker.SetActive(false);
            questMarker.SetActive(false);
        }
    }
}
