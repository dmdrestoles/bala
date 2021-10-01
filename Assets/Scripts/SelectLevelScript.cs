using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelScript : MonoBehaviour
{
    public void SelectLevelOne()
    {

    }

    public void SelectLevelTwo()
    {
        Debug.Log("Loading scene");
        SceneManager.LoadScene("DMScene2");
        Debug.Log("Loading completed");
    }
}
