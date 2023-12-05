using UnityEngine;
using UnityEngine.Video;
using System.Collections;

public class PlayVideoAndReturn : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;

    void Start()
    {
        // Suscribirse al evento cuando el video alcanza su punto final
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += EndReached;
        }
    }

    public void PlayVideo()
    {
        // Desactivar el canvas antes de reproducir el video
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        if (videoPlayer != null)
        {
            videoPlayer.Play();
            StartCoroutine(PlayVideoAndLoadSceneRoutine());
        }
    }

    IEnumerator PlayVideoAndLoadSceneRoutine()
    {
        // Espera hasta que el video termine de reproducirse
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }

        // Reactiva el canvas una vez que el video ha terminado
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }

    void EndReached(VideoPlayer vp)
    {
        // Reactiva el canvas cuando el video alcanza su punto final
        if (canvas != null)
        {
            canvas.SetActive(true);
        }
    }
}
