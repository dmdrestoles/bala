using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;

    public float restartDelay = 2f;
    public static bool IsInputEnabled = true;

    public GameObject reachedBridgeUI;

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
        Invoke("Restart", restartDelay);
    }

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER!");
            Invoke("Restart", restartDelay);
        }
    }

    void LoadLevelTwo()
    {
        SceneManager.LoadScene("DMScene2");
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
