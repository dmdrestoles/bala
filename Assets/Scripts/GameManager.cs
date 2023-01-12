using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;
    public static bool IsInputEnabled = true;

    public GameObject reachedBridgeUI;
    public GameObject playerDiedUI;
    public GameObject mainCanvas;
    public GameObject refObjectiveMarker;
    public static GameObject objectiveMarker;
    public static GameObject objectiveUI;

    // Objectives related
    public static int mainObjective, easterEggObjectives, rifleObjective, revolverObjective, ghostObjective, pacifistObjective, killObjective;

    [HideInInspector]
    public static string currentScene = "MainMenu";
    static GameManager gameManager;

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
        Init();


        //Call the Coroutine
        gameManager.StartCoroutine(Wait());
    }

    void Awake()
    {
        Debug.Log("GameManager ready.");
        objectiveUI = mainCanvas.transform.GetChild(7).gameObject;
        objectiveMarker = refObjectiveMarker;
    }

    public void CompleteLevelOne()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the marker!";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the marker!");
        Invoke("LoadLevelTwo", restartDelay);
    }

    public void CompleteLevelTwo()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the marker!";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the marker!");
        Invoke("LoadLevelThree", restartDelay);
    }

    public void CompleteLevelThree()
    {
        reachedBridgeUI.transform.GetChild(1).GetComponent<Text>().text = "You have reached the bridge!";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the bridge!");
        Invoke("LoadLevelFour", restartDelay);
    }

    public void CompleteLevelFour()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have escaped the Spanish encirclement!\nReturning to Main Menu...";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the safehouse!");
        Invoke("LoadMainMenu", 3f);
    }

    public void CompleteLevelFive()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have escaped the Spanish encirclement!\nReturning to Level Summary...";
        reachedBridgeUI.SetActive(true);
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
        mainObjective = 0;
        easterEggObjectives = 0;
        rifleObjective = 0;
        revolverObjective = 0;
        pacifistObjective = 1;
        ghostObjective = 1;
        killObjective = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            GameManager.PerformCoroutine();
            objectiveMarker.SetActive(true);
        }

        else if (objectiveName == "Collectible")
        {
            easterEggObjectives += 1;
            objectiveUI.GetComponent<Text>().text = "Easter Eggs Collected: " + easterEggObjectives + "/6";
            objectiveUI.SetActive(true);
            GameManager.PerformCoroutine();
        }

        else if (objectiveName == "M93")
        {
            rifleObjective = 1;
            objectiveUI.GetComponent<Text>().text = "M93 Taken (1/1)";
            objectiveUI.SetActive(true);
            GameManager.PerformCoroutine();
        }

        else if (objectiveName == "Revolver")
        {
            revolverObjective = 1;
            objectiveUI.GetComponent<Text>().text = "Revolver Taken (1/1)";
            objectiveUI.SetActive(true);
            GameManager.PerformCoroutine();
        }
    }

    public static IEnumerator Wait()
    {
        Debug.Log("Start waiting");
        yield return new WaitForSeconds(3);
        GameManager.objectiveUI.GetComponent<Text>().text = "";
        GameManager.objectiveUI.SetActive(false);
        Debug.Log("End waiting");
    }
}
