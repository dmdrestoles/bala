using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject start, level2, level3;
    void Start()
    {
        if (GameManager.currentScene == "level2")
        {
            start.SetActive(false);
            level2.SetActive(true);
        } else if (GameManager.currentScene == "level3")
        {
            start.SetActive(false);
            level3.SetActive(true);
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
