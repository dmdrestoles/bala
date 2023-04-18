using UnityEngine;
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
    public static GameObject objectivePanel;
    public GameObject fade;

    // Objectives related
    [Header("Objectives Scoring")]

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
        objectiveUI = mainCanvas.transform.Find("Objectives").gameObject;
        objectivePanel = mainCanvas.transform.Find("objPanel").gameObject;
        objectiveMarker = refObjectiveMarker;
        // ResetPreferences();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ResetPreferences();
        }

        if (Input.GetKeyDown(KeyCode.F12))
        {
            Debug.Log("Main Objective: " + PlayerPrefs.GetInt("isLetter4Found"));
            Debug.Log("Easter Eggs: " + PlayerPrefs.GetInt("easterEggObjective"));
            Debug.Log("M93: " + PlayerPrefs.GetInt("isM93Found"));
            Debug.Log("Revolver: " + PlayerPrefs.GetInt("isRevolverFound"));
            Debug.Log("Ghost: " + PlayerPrefs.GetInt("ghostObjective"));
            Debug.Log("Pacifist: " + PlayerPrefs.GetInt("pacifistObjective"));
            Debug.Log("Kills: " + PlayerPrefs.GetInt("killObjective"));
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
        PlayerPrefs.SetInt("isGameDone", 1);
        
        fade.SetActive(true);
        Invoke("LoadLevelSummary", 3f);
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            playerDiedUI.SetActive(true);
            gameHasEnded = true;
            Debug.Log("GAME OVER!");
            ResetPreferences();
            Invoke("Restart", restartDelay);
        }
    }

    void LoadMainMenu()
    {
        Debug.Log("LOADING TO MAIN MENU");
        InitializePreferences();
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
        InitializePreferences();
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelSummary()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        currentScene = "levelSummary";
        SceneManager.LoadScene("LevelSummary");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }

    void Restart()
    {
        ResetPreferences();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void InitializePreferences()
    {
        PlayerPrefs.SetInt("easterEggObjective",0);
        PlayerPrefs.SetInt("isM93Found",0);
        PlayerPrefs.SetInt("isRevolverFound",0);
        PlayerPrefs.SetInt("pacifistObjective",1);
        PlayerPrefs.SetInt("ghostObjective",1);
        PlayerPrefs.SetInt("killObjective",0);
        PlayerPrefs.SetInt("isGameDone", 0);

        //Debug.Log("Debug: " + "PlayerPrefs - Pacifist: " + PlayerPrefs.GetInt("pacifistObjective"));
        //Debug.Log("Debug: " + "PlayerPrefs - ghost: " + PlayerPrefs.GetInt("ghostObjective"));

        // Debug.Log("Debug: " + "PlayerPrefs - Pacifist: " + pacifistObjective);
        // Debug.Log("Debug: " + "PlayerPrefs - ghost: " + ghostObjective);
    }

    void ResetPreferences()
    {
        Debug.Log("Resetting game preferences");
        Melee.isBoloAcquired = false;
        PlayerPrefs.SetInt("pacifistObjective",1);
        PlayerPrefs.SetInt("ghostObjective",1);
        PlayerPrefs.SetInt("killObjective",0);
        PlayerPrefs.SetInt("isGameDone", 0);
    }

    public static void UpdateObjective(string objectiveName)
    {
        if (objectiveName == "Main")
        {
            AudioSource objectiveCompleteSFX = GameObject.Find("ObjectiveCompleteSFX").GetComponent<AudioSource>();

            objectiveUI.GetComponent<Text>().text = "Get the Letter and Escape (1/1)";
            objectiveCompleteSFX.Play();
            objectiveUI.SetActive(true);
            objectivePanel.SetActive(true);
            objectiveMarker.SetActive(true);
        }

        else if (objectiveName == "Collectible")
        {
            Debug.Log(PlayerPrefs.GetInt("easterEggObjective"));
            // int numOfEasterEggs = PlayerPrefs.GetInt("easterEggObjective") + 1;
            int easterEggObjectives = PlayerPrefs.GetInt("isLetter1Found") + PlayerPrefs.GetInt("isLetter2Found") + PlayerPrefs.GetInt("isLetter3Found") + PlayerPrefs.GetInt("isLetter5Found") + PlayerPrefs.GetInt("isCrossFound") + PlayerPrefs.GetInt("isCedulaFound");
            PlayerPrefs.SetInt("easterEggObjective", easterEggObjectives);
            objectiveUI.GetComponent<Text>().text = "Easter Eggs Collected: " + PlayerPrefs.GetInt("easterEggObjective") + "/6";
            objectiveUI.SetActive(true);
            objectivePanel.SetActive(true);
        }

        else if (objectiveName == "M93" && PlayerPrefs.GetInt("isM93Found") != 1)
        {
            PlayerPrefs.SetInt("isM93Found", 1);
            objectiveUI.GetComponent<Text>().text = "M93 Taken (1/1)";
            objectiveUI.SetActive(true);
            objectivePanel.SetActive(true);
        }

        else if (objectiveName == "Revolver" && PlayerPrefs.GetInt("isRevolverFound") != 1)
        {
            PlayerPrefs.SetInt("isRevolverFound", 1);
            objectiveUI.GetComponent<Text>().text = "Revolver Taken (1/1)";
            objectiveUI.SetActive(true);
            objectivePanel.SetActive(true);
        }
    }

    /*static IEnumerator DisplayObjective()
    {
        Debug.Log("Start waiting");
        yield return new WaitForSeconds(3);
        objectiveUI.GetComponent<Text>().text = "";
        objectiveUI.SetActive(false);
        objectivePanel.SetActive(true);
        Debug.Log("End waiting");
    }*/
}
