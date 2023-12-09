using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Llama a este m�todo para salir del juego
    public void QuitGame()
    {
        // Imprime un mensaje en la consola (esto no aparecer� en la versi�n final del juego, solo es �til para la depuraci�n)
        Debug.Log("Salida del juego");

        // Cierra la aplicaci�n
        Application.Quit();

        // Si est�s en el editor de Unity, esto detendr� la reproducci�n
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
