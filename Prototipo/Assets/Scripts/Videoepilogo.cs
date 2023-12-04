using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Videoepilogo : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToLoad = "MenuInicio";
    public GameObject canvas;

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
        }
    }

    public void PlayVideoAndLoadScene()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    void EndReached(VideoPlayer vp)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}