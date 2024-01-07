using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class PlayVideoThenLoadScene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Arrastra aqu� tu VideoPlayer desde el Inspector
    public string sceneToLoad = "nivel1"; // Aseg�rate de que esta escena est� en tus Build Settings
    public GameObject canvas; // Arrastra aqu� tu objeto Canvas desde el Inspector
    public bool loadFromUrl = false; // Determina si cargar desde URL o no
    public string videoUrl = ""; // La URL del video

    public void PlayVideoAndLoadScene()
    {
        // Oculta el Canvas al comenzar la reproducci�n del video
        if (canvas != null)
        {
            canvas.SetActive(false);
        }

        StartCoroutine(PlayVideoAndLoadSceneRoutine());
    }

    IEnumerator PlayVideoAndLoadSceneRoutine()
    {
        if (loadFromUrl)
        {
            videoPlayer.url = videoUrl;
            videoPlayer.source = VideoSource.Url;
        }
        else
        {
            // El videoPlayer ya deber�a tener configurado el clip de video si no se usa URL
            videoPlayer.source = VideoSource.VideoClip;
        }

        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
        {
            yield return null; // Espera hasta que el video est� preparado
        }

        videoPlayer.Play();

        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}
