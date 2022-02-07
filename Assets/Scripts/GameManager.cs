using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 1f;
    public static bool IsInputEnabled = true;

    public GameObject reachedBridgeUI;
    public GameObject playerDiedUI;

    public void CompleteLevelOne()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have reached the marker!";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the marker!");
        Invoke("LoadLevelTwo", restartDelay);
    }

    public void CompleteLevelTwo()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have reached the bridge!";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the bridge!");
        Invoke("LoadLevelThree", restartDelay);
    }

    public void CompleteLevelThree()
    {
        reachedBridgeUI.transform.GetChild(0).GetComponent<Text>().text = "You have escaped the Spanish encirclement!\nReturning to Main Menu...";
        reachedBridgeUI.SetActive(true);
        Debug.Log("You have reached the safehouse!");
        Invoke("LoadMainMenu", 3f);
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
        SceneManager.LoadScene("MainMenu");
    }

    void LoadLevelTwo()
    {
        SceneManager.LoadScene("Level2");
    }

    void LoadLevelThree()
    {
        SceneManager.LoadScene("Level3.1");
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
