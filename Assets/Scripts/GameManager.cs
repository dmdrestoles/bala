﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    [Header("Game States")]
    bool gameHasEnded = false;

    [Header("Game Values")]
    public float restartDelay = 3f;
    public static bool IsInputEnabled = true;

    [Header("Object References")]
    public GameObject reachedBridgeUI;
    public GameObject playerDiedUI;
    public GameObject mainCanvas;
    public GameObject refObjectiveMarker;
    public static GameObject objectiveMarker;
    public static GameObject objectiveUI;
    public GameObject fade;

    // Objectives related
    [Header("Objectives Scoring")]
    public static int mainObjective, easterEggObjectives, rifleObjective, revolverObjective, ghostObjective, pacifistObjective, killObjective;

    [HideInInspector]
    public static string currentScene = "MainMenu";
    static GameManager gameManager;
    static float elapsed = 0.0f;

    private static void Init()
    {
        if (gameManager == null)
        {
            GameObject go = new GameObject("Game Manager");
            gameManager = go.AddComponent<GameManager>();
        }
    }

    public static void PerformCoroutine()
    {
        //Call the Initialization
        //Init();


        //Call the Coroutine
       // gameManager.StartCoroutine(Wait());
    }

    void Start()
    {   
        if (SceneManager.GetActiveScene().name == "Level2Final")
        {
            Melee.isBoloAcquired = true;
            Debug.Log("Bolo activated!");
        }
    }

    void Awake()
    {
        Debug.Log("GameManager ready.");
        objectiveUI = mainCanvas.transform.GetChild(10).gameObject;
        objectiveMarker = refObjectiveMarker;
        InitializePreferences();
        // ResetPreferences();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ResetPreferences();
        }
    }

    public void CompleteLevelOne()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the marker!";
        reachedBridgeUI.SetActive(true);

        fade.SetActive(true);
        Debug.Log("You have escaped the firefight!");
        Invoke("LoadLevelTwo", restartDelay);
    }

    public void CompleteLevelTwo()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the marker!";
        reachedBridgeUI.SetActive(true);

        fade.SetActive(true);
        Debug.Log("You have escaped the forest!");
        Invoke("LoadOutpostLevel", restartDelay);
    }

    public void CompleteLevelThree()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the bridge!";
        reachedBridgeUI.SetActive(true);

        fade.SetActive(true);
        Debug.Log("You have reached the bridge!");
        Invoke("LoadLevelFour", restartDelay);
    }

    public void CompleteLevelFour()
    {
        //reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have escaped the Spanish encirclement!\nReturning to Main Menu...";
        //reachedBridgeUI.SetActive(true);
        //Debug.Log("You have reached the safehouse!");

        Invoke("LoadMainMenu", 0.2f);
    }

    public void CompleteLevelFive()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have escaped the Spanish encirclement!\nReturning to Level Summary...";
        reachedBridgeUI.SetActive(true);
        
        fade.SetActive(true);
        PlayerPrefs.SetInt("easterEggObjective",easterEggObjectives);
        PlayerPrefs.SetInt("rifleObjective",rifleObjective);
        PlayerPrefs.SetInt("revolverObjective",revolverObjective);
        PlayerPrefs.SetInt("pacifistObjective",pacifistObjective);
        PlayerPrefs.SetInt("ghostObjective",ghostObjective);
        PlayerPrefs.SetInt("killObjective",killObjective);
        Invoke("LoadLevelSummary", 3f);
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            playerDiedUI.SetActive(true);
            gameHasEnded = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartDelay);
        }
    }

    void LoadMainMenu()
    {
        Debug.Log("LOADING TO MAIN MENU");
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "MainMenu";
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelTwo()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "level2";
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelThree()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "level3";
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelFour()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "level4";
        SceneManager.LoadScene("MainMenu");
    }

    void LoadOutpostLevel()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "outpostLevel";
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelSummary()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "levelSummary";
        SceneManager.LoadScene("LevelSummary");
        InitializePreferences();
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }

    void Restart()
    {
        mainObjective = 0;
        /*
        easterEggObjectives = 0;
        rifleObjective = 0;
        revolverObjective = 0;
        pacifistObjective = 1;
        ghostObjective = 1;
        killObjective = 0;
        */
        InitializePreferences();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void InitializePreferences()
    {
        easterEggObjectives = PlayerPrefs.GetInt("easterEggObjective",0);
        rifleObjective = PlayerPrefs.GetInt("rifleObjective",0);
        revolverObjective = PlayerPrefs.GetInt("revolverObjective",0);
        pacifistObjective = PlayerPrefs.GetInt("pacifistObjective",1);
        ghostObjective  = PlayerPrefs.GetInt("ghostObjective",1);
        killObjective = PlayerPrefs.GetInt("killObjective",0);

        //Debug.Log("Debug: " + "PlayerPrefs - Pacifist: " + PlayerPrefs.GetInt("pacifistObjective"));
        //Debug.Log("Debug: " + "PlayerPrefs - ghost: " + PlayerPrefs.GetInt("ghostObjective"));

        Debug.Log("Debug: " + "PlayerPrefs - Pacifist: " + pacifistObjective);
        Debug.Log("Debug: " + "PlayerPrefs - ghost: " + ghostObjective);
    }

    void ResetPreferences()
    {
        Debug.Log("Resetting game preferences");
        PlayerPrefs.SetInt("easterEggObjective",0);
        PlayerPrefs.SetInt("rifleObjective",0);
        PlayerPrefs.SetInt("revolverObjective",0);
        PlayerPrefs.SetInt("pacifistObjective",1);
        PlayerPrefs.SetInt("ghostObjective",1);
        PlayerPrefs.SetInt("killObjective",0);
    }

    public static void UpdateObjective(string objectiveName)
    {
        if (objectiveName == "Main")
        {
            AudioSource objectiveCompleteSFX = GameObject.Find("ObjectiveCompleteSFX").GetComponent<AudioSource>();

            mainObjective = 1;
            objectiveUI.GetComponent<Text>().text = "Get the Letter and Escape (1/1)";
            objectiveCompleteSFX.Play();
            objectiveUI.SetActive(true);
            objectiveMarker.SetActive(true);
        }

        else if (objectiveName == "Collectible" && easterEggObjectives < 6)
        {
            easterEggObjectives += 1;
            objectiveUI.GetComponent<Text>().text = "Easter Eggs Collected: " + easterEggObjectives + "/6";
            objectiveUI.SetActive(true);
        }

        else if (objectiveName == "M93" && rifleObjective == 0)
        {
            rifleObjective = 1;
            objectiveUI.GetComponent<Text>().text = "M93 Taken (1/1)";
            objectiveUI.SetActive(true);
        }

        else if (objectiveName == "Revolver" && revolverObjective == 0)
        {
            revolverObjective = 1;
            objectiveUI.GetComponent<Text>().text = "Revolver Taken (1/1)";
            objectiveUI.SetActive(true);
        }
    }

    static IEnumerator DisplayObjective()
    {
        Debug.Log("Start waiting");
        yield return new WaitForSeconds(3);
        objectiveUI.GetComponent<Text>().text = "";
        objectiveUI.SetActive(false);
        Debug.Log("End waiting");
    }
}
