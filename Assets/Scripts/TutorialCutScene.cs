using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialCutScene : MonoBehaviour
{
    public GameObject PlayerCam;
    public GameObject Cam1;
    public GameObject Cam2;
    public GameObject Cam3;
    public GameObject Cam4;
    public GameObject canvas;
    public GameObject minimarker;
    public GameObject objectiveMarker;
    public GameObject Player;
    public Animator officer;
    public GameObject FPPlayer;
    public GameObject playerHolder;
    public GameObject textHolder;
    public GameObject textHolder1;

    //Text Objects
    public GameObject speaker; //4
    public GameObject dialogue; //5
    public GameObject instructions; //6
    public GameObject objectives; //7


    public static bool reachedMarker = false;
    public static bool reachedLastMarker = false;

    // Start is called before the first frame update
    void Start()
    {
        speaker.GetComponent<Text>().text = "";
        dialogue.GetComponent<Text>().text = "";
        instructions.GetComponent<Text>().text = "";
        objectives.GetComponent<Text>().text = "";
        PlayerMovement.moveSpeed = 0;
        Cursor.visible = false;

        GameManager.IsInputEnabled = false;
        canvas.SetActive(false);
        StartCoroutine(TheSequence());
    }

    void Update()
    {
        if (speaker.GetComponent<Text>().text != "" || dialogue.GetComponent<Text>().text != "" || instructions.GetComponent<Text>().text != "")
        {
           textHolder.SetActive(true);
        }
        else
        {
            textHolder.SetActive(false);
        }

        if (objectives.GetComponent<Text>().text != "" )
        {
            textHolder1.SetActive(true);
        }
        else
        {
            textHolder1.SetActive(false);
        }
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(5);
        playerHolder.SetActive(false);
        FPPlayer.SetActive(true);
        PlayerCam.SetActive(true);
        Debug.Log("Player camera active");
        canvas.SetActive(true);
        Cam1.SetActive(false);
        GameManager.IsInputEnabled = true;
        yield return new WaitForSeconds(2);
        speaker.GetComponent<Text>().text = "Officer";
        dialogue.GetComponent<Text>().text = "Fuego!";
        yield return new WaitForSeconds(1);
        speaker.GetComponent<Text>().text = "";
        dialogue.GetComponent<Text>().text = "";
        instructions.GetComponent<Text>().text = "Use mouse movement and Mouse Right-Click to aim\nTo shoot, use Mouse Left-Click";

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                yield return new WaitForSeconds(0.5f);
                instructions.GetComponent<Text>().text = "Press R to reload";

                while (true)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        instructions.GetComponent<Text>().text = "";
                        yield return new WaitForSeconds(1);
                        speaker.GetComponent<Text>().text = "Player";
                        dialogue.GetComponent<Text>().text = "Shit! 4 bullets left.";
                        yield return new WaitForSeconds(4);
                        speaker.GetComponent<Text>().text = "";
                        dialogue.GetComponent<Text>().text = "";
                        instructions.GetComponent<Text>().text = "Use mouse movement to aim\nTo shoot, use Mouse Left-Click";
                        
                        while (true)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                instructions.GetComponent<Text>().text = "";
                                GameManager.IsInputEnabled = false;
                                yield return new WaitForSeconds(1);
                                playerHolder.SetActive(true);
                                Cam2.SetActive(true);
                                yield return new WaitForSeconds(1);
                                officer.SetTrigger("triggerDeath");
                                yield return new WaitForSeconds(1);
                                speaker.GetComponent<Text>().text = "Officer";
                                dialogue.GetComponent<Text>().text = "AAHH!";
                                yield return new WaitForSeconds(1.5f);
                                dialogue.GetComponent<Text>().text = "[PLAYER]! Ven rapido!";
                                yield return new WaitForSeconds(1.5f);
                                speaker.GetComponent<Text>().text = "";
                                dialogue.GetComponent<Text>().text = "";
                                Cam2.SetActive(false);
                                PlayerCam.SetActive(true);
                                Player.SetActive(true);
                                playerHolder.SetActive(true);
                                GameManager.IsInputEnabled = true;
                                PlayerMovement.moveSpeed = 12f;
                                instructions.GetComponent<Text>().text = "Use W-A-S-D to move.";
                                objectives.GetComponent<Text>().text = "Go to the Officer";
                                minimarker.SetActive(true);

                                while (true)
                                {
                                    if (reachedMarker == true)
                                    {
                                        Debug.Log("Marker Reached");
                                        minimarker.SetActive(false);
                                        instructions.GetComponent<Text>().text = "";
                                        objectives.GetComponent<Text>().text = "";
                                        //yield return new WaitForSeconds(0.5f);
                                        GameManager.IsInputEnabled = false;
                                        Cam3.SetActive(true);
                                        yield return new WaitForSeconds(1);
                                        speaker.GetComponent<Text>().text = "Officer";
                                        dialogue.GetComponent<Text>().text = "You need to go! Find Sancho Valenzuela and warn him of what happened here!";
                                        Debug.Log("Text Changed");
                                        yield return new WaitForSeconds(5);
                                        Cam4.SetActive(true); 
                                        Cam3.SetActive(false);
                                        yield return new WaitForSeconds(1);
                                        speaker.GetComponent<Text>().text = "Spanish Soldier";
                                        dialogue.GetComponent<Text>().text = "Ataque!";
                                        yield return new WaitForSeconds(1);
                                        PlayerPrefs.SetInt("Secondary", 3);
                                        //Player.transform.parent.GetChild(1).GetChild(0).GetComponent<WeaponSwitch>().SetSecondary();
                                        objectiveMarker.SetActive(true);
                                        speaker.GetComponent<Text>().text = "";
                                        dialogue.GetComponent<Text>().text = "";
                                        GameManager.IsInputEnabled = true;
                                        PlayerCam.SetActive(true);
                                        Cam4.SetActive(false);
                                        objectives.GetComponent<Text>().text = "Go to the Marker!";
                                        instructions.GetComponent<Text>().text = "Press Shift to run\nWait for 5 seconds";

                                        while (true)
                                        {
                                            if (reachedLastMarker == true)
                                            {
                                                objectives.GetComponent<Text>().text = "";
                                            }
                                            yield return null;
                                        }
                                    }
                                    yield return null;
                                }

                            }
                                yield return null;
                        }
                        
                    }

                    yield return null;
                }
            }

            yield return null;
        }
        
    }
}

/*
public void NextObjective()
    {
        float distanceToTarget = Vector3.Distance(player.transform.position, objective.transform.position);
        if (distanceToTarget <= 2)
        {

        }
    }

    public GameObject objective;
    public GameObject player;
 
*/