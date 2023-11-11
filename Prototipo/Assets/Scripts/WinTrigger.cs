using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el jugador colision� con la estrella
        if (other.CompareTag("Player"))
        {
            // Carga la escena de victoria
            SceneManager.LoadScene("WinScene");
        }
    }
}
