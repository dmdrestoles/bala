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
        sceneName = "Level1Final";
        PlayerPrefs.SetInt("Primary", 2);
    }

    public void SelectLevelTwo()
    {
        sceneName = "Level2Final";
    }

    public void SelectLevelThree()
    {
        sceneName = "Level3Final";
    }

    public void SelectLevelFour()
    {
        sceneName = "Level4Final";
    }

    public void setSceneName(string name)
    {
        sceneName = name;
    }

    public void PlayScene()
    {
        // DontDestroyOnLoad(playerWeapons);
        GetActiveWeapons();
        SceneManager.LoadScene(sceneName);
    }   

    // IEnumerator LoadScene(string name)
    // {
    //     yield return null;

    //     Scene currentScene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(name, LoadSceneMode.Additive);

    //     Scene sceneToLoad = SceneManager.GetSceneByName(name);

    //     SceneManager.MoveGameObjectToScene(playerWeapons, sceneToLoad);

    //     yield return null;

    //     SceneManager.UnloadSceneAsync(currentScene);

    // }

    void GetActiveWeapons()
    {
        for (int i = 0; i < playerWeapons.transform.childCount - 1; i ++)
        {
            GameObject weapon = playerWeapons.transform.GetChild(i).gameObject;

            if (weapon.activeSelf)
            {
                if (weapon.GetComponent<Weapon>().type == "Primary")
                {
                    PlayerPrefs.SetInt("Primary", i);
                }
                else if (weapon.GetComponent<Weapon>().type == "Secondary")
                {
                    PlayerPrefs.SetInt("Secondary", i);
                }
                else if (weapon.GetComponent<Weapon>().type == "Melee")
                {
                    PlayerPrefs.SetInt("Melee", i);
                }
            }
            PlayerPrefs.Save();
        }
    }
}
