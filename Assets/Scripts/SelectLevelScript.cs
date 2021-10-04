using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelScript : MonoBehaviour
{
    public string sceneName;
    public GameObject playerWeapons;
    private AsyncOperation sceneAsync;

    public void SelectLevelOne()
    {
        sceneName = "tutorial";
    }

    public void SelectLevelTwo()
    {
        sceneName = "DMScene2";
    }

    public void setSceneName(string name)
    {
        sceneName = name;
    }

    public void PlayScene()
    {
        DontDestroyOnLoad(playerWeapons);
        StartCoroutine(LoadScene(sceneName));
    }   

    IEnumerator LoadScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);

        Scene currentScene = SceneManager.GetActiveScene();
        Scene sceneToLoad = SceneManager.GetSceneByName(name);

        SceneManager.MoveGameObjectToScene(playerWeapons, sceneToLoad);

        yield return null;

        SceneManager.UnloadSceneAsync(currentScene);
        
    }
}
