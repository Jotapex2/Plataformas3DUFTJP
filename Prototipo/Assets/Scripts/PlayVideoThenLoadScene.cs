using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class PlayVideoThenLoadScene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Arrastra aqu� tu VideoPlayer desde el Inspector
    public string sceneToLoad = "nivel1"; // Aseg�rate de que esta escena est� en tus Build Settings
    public GameObject canvas; // Arrastra aqu� tu objeto Canvas desde el Inspector

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
        videoPlayer.Play();
        while (videoPlayer.isPlaying)
        {
            yield return null;
        }
        SceneManager.LoadScene(sceneToLoad);
    }
}