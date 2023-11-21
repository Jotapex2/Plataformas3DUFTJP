using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class PlayVideoThenLoadScene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Arrastra aquí tu VideoPlayer desde el Inspector
    public string sceneToLoad = "nivel1"; // Asegúrate de que esta escena esté en tus Build Settings
    public GameObject canvas; // Arrastra aquí tu objeto Canvas desde el Inspector

    public void PlayVideoAndLoadScene()
    {
        // Oculta el Canvas al comenzar la reproducción del video
        if (canvas != null)
        {
            canvas.SetActive(false);
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
        SceneManager.LoadScene(sceneToLoad);
    }
}