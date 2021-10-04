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
        StartCoroutine(LoadScene(sceneName));
    }   

    IEnumerator LoadScene(string name)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log( currentScene.name );
        AsyncOperation scene = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
        sceneAsync = scene;

        while ( scene.progress < 0.9f )
        {
            Debug.Log("Loading scene " + " [][] Progress: " + scene.progress);
            yield return null;
        }
        yield return new WaitForSeconds(2.0f);
        OnFinishedLoadingAllScene();
    }

    void EnableScene(string name)
    {
        sceneAsync.allowSceneActivation = true;

        Scene sceneToLoad = SceneManager.GetSceneByName(name);

        if ( sceneToLoad.IsValid() )
        {
            Debug.Log("Scene is valid, loading.");
            SceneManager.MoveGameObjectToScene(playerWeapons, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);
        }
    }

    void OnFinishedLoadingAllScene()
    {
        Debug.Log("Done Loading Scene.");
        EnableScene(sceneName);
        Debug.Log("Scene activated!");
    }
}
