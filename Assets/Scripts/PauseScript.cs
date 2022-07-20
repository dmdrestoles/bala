using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    GameObject pauseCanvas, mainCanvas;
    public GameObject codexCanvas;
    bool paused = false;
    bool canvasOpen = false;
    private GameObject soundSlider, fovSlider, senseSlider;
    private float sound, fov, sense;
    static float currFOV;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.Find("PauseCanvas");
        mainCanvas = GameObject.Find("MainCanvas");
        pauseCanvas.gameObject.SetActive (false);

        Debug.Log("PS: FOV on start" + PlayerPrefs.GetFloat("fov"));
        currFOV = PlayerPrefs.GetFloat("fov");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && paused == false)
        {
            PauseGame();
        }

        else if(Input.GetKeyUp(KeyCode.Tab) && paused == true)
        {
            if (canvasOpen == false)
            {
                ResumeGame();
            }
        }
        
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        paused = true;
        pauseCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        UpdateSliders();
        GameManager.IsInputEnabled = false;
        Cursor.lockState = CursorLockMode.Confined ;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1.0f;
        pauseCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        GameManager.IsInputEnabled = true;
        Cursor.lockState = CursorLockMode.Locked ;
        Cursor.visible = false;
    }

    private void UpdateSliders()
    {
        //sound = PlayerPrefs.GetFloat("sound", 50); 
        sense = PlayerPrefs.GetFloat("sense", 50);
        fov = PlayerPrefs.GetFloat("fov",50);

        //soundSlider = GameObject.Find("SoundSlider");
        senseSlider = GameObject.Find("SenseSlider");
        fovSlider = GameObject.Find("FOVSlider");

        //soundSlider.GetComponent<Slider>().value = sound;
        senseSlider.GetComponent<Slider>().value = sense;
        fovSlider.GetComponent<Slider>().value = fov;

        Debug.Log("PS: current FOV = " + fov);
    }

    private void FindSliderObjects()
    {
        
    }

    public void SavePrefs()
    {
        //PlayerPrefs.SetFloat("sound", soundSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("sense", senseSlider.GetComponent<Slider>().value);
        PlayerPrefs.SetFloat("fov", fovSlider.GetComponent<Slider>().value);
        PlayerPrefs.Save();

        currFOV = PlayerPrefs.GetFloat("fov");
    }

    public void ApplyPrefs()
    {
        SavePrefs();
        ResumeGame();
    }

    public void OpenCodex()
    {
        Time.timeScale = 0f;
        paused = true;
        canvasOpen = true;

        pauseCanvas.gameObject.SetActive(false);
        codexCanvas.gameObject.SetActive(true);

        GameManager.IsInputEnabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void CloseCodex()
    {
        Time.timeScale = 0f;
        paused = true;
        canvasOpen = false;

        codexCanvas.gameObject.SetActive(false);
        pauseCanvas.gameObject.SetActive(true);

        GameManager.IsInputEnabled = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public static float getFOV()
    {
        return currFOV;
    }
}
