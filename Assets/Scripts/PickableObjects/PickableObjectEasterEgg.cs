using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PickableObjectEasterEgg : PickableObjectStateManager
{
    public GameObject codexNotif;
    public GameObject codex;
    public Sprite easterEggSprite;

    public override void HandleObjectPickup(string objectName)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Picked up object: " + objectName);

            // Add to codex here
            if (!codex.active)
            {
                codex.SetActive(true);
                codexNotif.SetActive(true);
                codexNotif.transform.GetChild(1).GetComponent<Image>().sprite = easterEggSprite;
                codexNotif.transform.GetChild(2).GetComponent<Text>().text = objectName;
            }

            Destroy(gameObject);
        }
    }
}
