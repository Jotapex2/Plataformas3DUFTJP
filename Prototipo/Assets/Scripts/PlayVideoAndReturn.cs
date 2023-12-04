using UnityEngine;
using UnityEngine.Video;
using System.Collections;
public class PlayVideoAndReturn : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;
    public Camera mainCamera; // Aseg�rate de asignar la c�mara principal desde el Inspector

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
        }
    }

    public void PlayVideo()
    {
        if (canvas != null)
        {
            canvas.SetActive(false);
        }
        /*
        if (mainCamera != null)
        {
            mainCamera.enabled = false; // Desactiva la c�mara principal
        }
        */
        if (videoPlayer != null)
        {
            videoPlayer.Play();
        }
        StartCoroutine(PlayVideoAndLoadSceneRoutine());
    }
    IEnumerator PlayVideoAndLoadSceneRoutine()
    {
        videoPlayer.Play();
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        canvas.SetActive(true);
    }
    void EndReached(VideoPlayer vp)
    {
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
        /*
        if (mainCamera != null)
        {
            mainCamera.enabled = true; // Reactiva la c�mara principal
        }
        */
    }
}
