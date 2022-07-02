using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public GameObject selectedObject;
    public int redCol, blueCol, greenCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;

    // Update is called once per frame
    void Update()
    {
        if (lookingAtObject && gameObject.transform.name == MouseLook.selectedObject)
        {
            Debug.Log(gameObject.transform.name);
            selectedObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
            selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(
                (byte)redCol,
                (byte)greenCol,
                (byte)blueCol,255) );
        }
    }

    void OnMouseOver()
    {
        selectedObject = GameObject.Find(MouseLook.selectedObject);
        if (gameObject.transform.name == MouseLook.selectedObject)
        {
            lookingAtObject = true;
            HandleObjectPickup(MouseLook.selectedObject);
            if(!startedFlashing)
            {
                startedFlashing = true;
                StartCoroutine(FlashObject());
            }
        }
        
    }

    void OnMouseExit()
    {
        startedFlashing = false;
        lookingAtObject = false;
        StopCoroutine(FlashObject());
        selectedObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color32(0,0,0,0));
    }

    private void HandleObjectPickup(string objectName)
    {
        if (Input.GetKeyDown(KeyCode.F) && !Melee.isBoloAcquired)
            {
                selectedObject.SetActive(false);
                if (objectName == "Bolo")
                {
                    Melee.isBoloAcquired = true;
                }
            }
    }

    IEnumerator FlashObject()
    {
        while(lookingAtObject == true)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn == true)
            {
                if (blueCol <= 0)
                {
                    flashingIn = false;
                }
                else
                {
                    blueCol -= 4;
                    greenCol -= 4;
                    redCol -= 4;
                }
            } else
            {
                if (blueCol >= 100)
                {
                    flashingIn = true;
                }
                else
                {
                    blueCol += 4;
                    greenCol += 4;
                    redCol += 4;
                }
            }
        }
    }
}
