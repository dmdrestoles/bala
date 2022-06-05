using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StealthLevelScript : MonoBehaviour
{
    public GameObject canvas;
    public GameObject Player;
    public GameObject textHolder;
    public GameObject textHolder1;
    
    private PlayerState playerState;
    private bool stealthTrigger = false;


    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
        canvas.transform.GetChild(6).GetComponent<Text>().text = "";
        canvas.transform.GetChild(7).GetComponent<Text>().text = "";
        playerState = Player.GetComponent<PlayerState>();
        StartCoroutine(StartLevelSequence());
    }

    void Update()
    {
        if (GameManager.IsInputEnabled)
        {
            if (canvas.transform.GetChild(4).GetComponent<Text>().text != "" || canvas.transform.GetChild(5).GetComponent<Text>().text != "" || canvas.transform.GetChild(6).GetComponent<Text>().text != "")
            {
                textHolder.SetActive(true);
            }
            else
            {
                textHolder.SetActive(false);
            }

            if (canvas.transform.GetChild(7).GetComponent<Text>().text != "" )
            {
                textHolder1.SetActive(true);
            }
            else
            {
                textHolder1.SetActive(false);
            }

            if (!playerState.isVisible && !stealthTrigger)
            {
                StartCoroutine(UseSecondaryWeapon());
                stealthTrigger = true;
            }    
        }
        
    }

    IEnumerator StartLevelSequence()
    {
        GameManager.IsInputEnabled = true;
        yield return new WaitForSeconds(1.0f);
        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
        canvas.transform.GetChild(6).GetComponent<Text>().text = "An Enemy in front of me\n I should take him out quietly.";
        yield return new WaitForSeconds(5.0f);
        canvas.transform.GetChild(6).GetComponent<Text>().text = "Press Left CTRL to toggle crouch. \nTo melee, press V.";
        yield return new WaitForSeconds(5.0f);
        canvas.transform.GetChild(6).GetComponent<Text>().text = "";

        yield return null;
    }

    IEnumerator UseSecondaryWeapon()
    {
        yield return new WaitForSeconds(1.0f);
        canvas.transform.GetChild(6).GetComponent<Text>().text = "While crouched and in bushes, enemies cannot see you.";
        yield return new WaitForSeconds(4.0f);
        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
        canvas.transform.GetChild(6).GetComponent<Text>().text = "Press 2 to switch to sumpit.\nYou need to aim the sumpit to fire.";
        yield return new WaitForSeconds(3.0f);
        canvas.transform.GetChild(6).GetComponent<Text>().text = "The sumpit is a silent weapon that makes enemies unable to move or detect you.";
        yield return new WaitForSeconds(5.0f);
        canvas.transform.GetChild(6).GetComponent<Text>().text = "";
        yield return null;
    }
}