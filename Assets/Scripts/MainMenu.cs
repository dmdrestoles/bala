using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start, level2, level3, level4, outpostLevel, videoScriptObject;
    public VideoPlayerScript videoPlayerScript;
    void Start()
    {
        if (GameManager.currentScene == "level2")
        {
            videoScriptObject.SetActive(true);
            videoPlayerScript.PlayPaltikVid();
        } else if (GameManager.currentScene == "level3")
        {
            start.SetActive(false);
            level3.SetActive(true);
        } else if (GameManager.currentScene == "level4")
        {
            start.SetActive(false);
            level4.SetActive(true);
        }
        else if (GameManager.currentScene == "outpostLevel")
        {
            start.SetActive(false);
            outpostLevel.SetActive(true);
        }
    }
    public void PlayGame()
    {

    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }    
}
