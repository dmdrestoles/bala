using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoClip introVid, continueVid, paltikVid;
    public SelectLevelScript selectLevelScript;
    private GameObject mainCamera, mainCanvas;
    private VideoPlayer videoPlayer;
    private bool introPlayed = false;
    private bool paltikPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("MainCamera");
        mainCanvas = GameObject.Find("MainCanvas");
        videoPlayer = mainCamera.GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!videoPlayer.isPlaying && (introPlayed || paltikPlayed))
        {
            PlayContinueClip();
        }

        if(videoPlayer.clip == continueVid)
        {
            HandleContinueClip();
        }
        
    }

    public void PlayIntroVideo()
    {
        introPlayed = true;
        videoPlayer.Play();
        mainCanvas.SetActive(false);
    }

    public void PlayPaltikVid()
    {
        FindMainCanvasMaincamera();
        paltikPlayed = true;
        Debug.Log(videoPlayer.clip.name);
        videoPlayer.clip = paltikVid;
        videoPlayer.Play();
        mainCanvas.SetActive(false);
    }

    public void ChangeVideo(VideoClip clip)
    {
        videoPlayer.clip = clip;
    }

    public void PlayContinueClip()
    {
        videoPlayer.clip = continueVid;
        videoPlayer.isLooping = true;
        videoPlayer.Play();
    }

    private void HandleContinueClip()
    {
        if (Input.anyKey)
        {
            if (introPlayed)
            {
                selectLevelScript.SelectLevelOne();
                selectLevelScript.PlayScene();
            }
            
            else if (paltikPlayed)
            {
                selectLevelScript.SelectLevelTwo();
                selectLevelScript.PlayScene();
            }
        }
    }

    private void FindMainCanvasMaincamera()
    {
        mainCamera = GameObject.Find("MainCamera");
        mainCanvas = GameObject.Find("MainCanvas");
        videoPlayer = mainCamera.GetComponent<VideoPlayer>();
    }
}


