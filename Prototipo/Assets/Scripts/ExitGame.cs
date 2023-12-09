using UnityEngine;

public class ExitGame : MonoBehaviour
{
    // Llama a este método para salir del juego
    public void QuitGame()
    {
        // Imprime un mensaje en la consola (esto no aparecerá en la versión final del juego, solo es útil para la depuración)
        Debug.Log("Salida del juego");

        // Cierra la aplicación
        Application.Quit();

        // Si estás en el editor de Unity, esto detendrá la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
