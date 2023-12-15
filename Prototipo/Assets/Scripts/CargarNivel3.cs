using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class CargarNivel3 : MonoBehaviour
{

    public Slider progressBar;  // Referencia al componente Slider (Barra de progreso)

    public void Start()
    {
        StartLoading();
    }

    // Método para iniciar la carga de la escena
    public void LoadScene(string GameScene)
    {
        StartCoroutine(LoadSceneAsync(GameScene));
    }

    // Cambié el nombre del método de Start() a StartLoading()
    public void StartLoading()
    {
        StartCoroutine(LoadSceneAsync("Nivel3")); // Reemplaza "NombreDeTuEscena" con el nombre real de la escena.
    }

    private IEnumerator LoadSceneAsync(string GameScene)
    {
        // Inicia la carga de la escena de manera asíncrona
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameScene);

        // Evita que la escena se active automáticamente al terminar la carga
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            // Actualiza la barra de progreso
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            progressBar.value = progress;

            // Checa si la escena ha terminado de cargar (el valor de progress llega hasta 0.9)
            if (asyncOperation.progress >= 0.9f)
            {
                // Activa la escena cuando esté lista
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
