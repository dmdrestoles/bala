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

    public static bool reachedMarker = false;
    public static bool reachedLastMarker = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
        canvas.transform.GetChild(6).GetComponent<Text>().text = "";
        canvas.transform.GetChild(7).GetComponent<Text>().text = "";
        PlayerMovement.moveSpeed = 0;
        officer.enabled = false;

        GameManager.IsInputEnabled = false;
        canvas.SetActive(false);
        StartCoroutine(TheSequence());
    }

    IEnumerator TheSequence()
    {
        yield return new WaitForSeconds(5);
        PlayerCam.SetActive(true);
        canvas.SetActive(true);
        Cam1.SetActive(false);
        GameManager.IsInputEnabled = true;
        yield return new WaitForSeconds(2);
        canvas.transform.GetChild(4).GetComponent<Text>().text = "Officer";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "Fuego!";
        yield return new WaitForSeconds(1);
        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
        canvas.transform.GetChild(6).GetComponent<Text>().text = "Use mouse movement to aim\nTo shoot, use Mouse Left-Click";

        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                yield return new WaitForSeconds(0.5f);
                canvas.transform.GetChild(6).GetComponent<Text>().text = "Press R to reload";

                while (true)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        canvas.transform.GetChild(6).GetComponent<Text>().text = "";
                        yield return new WaitForSeconds(1);
                        canvas.transform.GetChild(4).GetComponent<Text>().text = "Player";
                        canvas.transform.GetChild(5).GetComponent<Text>().text = "Shit! 3 bullets left.";
                        yield return new WaitForSeconds(4);
                        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
                        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
                        canvas.transform.GetChild(6).GetComponent<Text>().text = "Use mouse movement to aim\nTo shoot, use Mouse Left-Click";
                        
                        while (true)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                canvas.transform.GetChild(6).GetComponent<Text>().text = "";
                                GameManager.IsInputEnabled = false;
                                yield return new WaitForSeconds(1);
                                PlayerCam.SetActive(false);
                                Cam2.SetActive(true);
                                yield return new WaitForSeconds(1);
                                officer.enabled = true;
                                yield return new WaitForSeconds(1);
                                canvas.transform.GetChild(4).GetComponent<Text>().text = "Officer";
                                canvas.transform.GetChild(5).GetComponent<Text>().text = "AAHH!";
                                yield return new WaitForSeconds(1.5f);
                                canvas.transform.GetChild(5).GetComponent<Text>().text = "[PLAYER]! Ven rapido!";
                                yield return new WaitForSeconds(1.5f);
                                canvas.transform.GetChild(4).GetComponent<Text>().text = "";
                                canvas.transform.GetChild(5).GetComponent<Text>().text = "";
                                Cam2.SetActive(false);
                                PlayerCam.SetActive(true);
                                GameManager.IsInputEnabled = true;
                                PlayerMovement.moveSpeed = 12f;
                                canvas.transform.GetChild(6).GetComponent<Text>().text = "Use W-A-S-D to move.";
                                canvas.transform.GetChild(7).GetComponent<Text>().text = "Go to the Officer";
                                minimarker.SetActive(true);

                                while (true)
                                {
                                    if (reachedMarker == true)
                                    {
                                        minimarker.SetActive(false);
                                        canvas.transform.GetChild(6).GetComponent<Text>().text = "";
                                        canvas.transform.GetChild(7).GetComponent<Text>().text = "";
                                        Player.SetActive(false);
                                        yield return new WaitForSeconds(0.5f);
                                        GameManager.IsInputEnabled = false;
                                        PlayerCam.SetActive(false);
                                        Cam3.SetActive(true);
                                        yield return new WaitForSeconds(1);
                                        canvas.transform.GetChild(4).GetComponent<Text>().text = "Officer";
                                        canvas.transform.GetChild(5).GetComponent<Text>().text = "You need to go! Find Sancho Valenzuela and warn him of what happened here!";
                                        yield return new WaitForSeconds(5);
                                        Cam4.SetActive(true); 
                                        Cam3.SetActive(false);
                                        yield return new WaitForSeconds(1);
                                        canvas.transform.GetChild(4).GetComponent<Text>().text = "Spanish Soldier";
                                        canvas.transform.GetChild(5).GetComponent<Text>().text = "Ataque!";
                                        yield return new WaitForSeconds(1);
                                        PlayerPrefs.SetInt("Secondary", 3);
                                        Player.transform.parent.GetChild(1).GetChild(0).GetComponent<WeaponSwitch>().SetSecondary();
                                        objectiveMarker.SetActive(true);
                                        canvas.transform.GetChild(4).GetComponent<Text>().text = "";
                                        canvas.transform.GetChild(5).GetComponent<Text>().text = "";
                                        Player.SetActive(true);
                                        GameManager.IsInputEnabled = true;
                                        PlayerCam.SetActive(true);
                                        Cam4.SetActive(false);
                                        canvas.transform.GetChild(7).GetComponent<Text>().text = "Go to the Marker!";

                                        while (true)
                                        {
                                            if (reachedLastMarker == true)
                                            {
                                                canvas.transform.GetChild(7).GetComponent<Text>().text = "";
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