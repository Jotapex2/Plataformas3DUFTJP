using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    public AudioClip soundClip; // Arrastra tu clip de audio aqu� a trav�s del Inspector
    private AudioSource audioSource;
    private bool isCollected = false; // Bandera para prevenir recolecciones m�ltiples
    public string winSceneName = "WinScene"; // Nombre de la escena de victoria, asignable en el Inspector


    void Start()
    {
        // Verifica si este GameObject tiene un AudioSource antes de intentar acceder a �l.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Si no hay ning�n AudioSource, imprime un mensaje de advertencia y agrega uno.
            Debug.LogWarning("No se encontr� AudioSource. Se agregar� uno autom�ticamente.", this);
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura el AudioSource
        audioSource.playOnAwake = false;
        audioSource.clip = soundClip;
        if (soundClip == null)
        {
            Debug.LogError("No se ha asignado clip de audio a soundClip en el Inspector.", this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true; // Establece la bandera para evitar recolecciones m�ltiples

            // Aseg�rate de que el clip de audio no sea nulo antes de intentar reproducirlo.
            if (audioSource.clip != null)
            {
                // Reproduce el sonido de recolecci�n de la estrella
                audioSource.Play();

                // Obtiene el componente CharacterStatus del jugador y aumenta la estrella.
                CharacterStatus characterStatus = other.GetComponent<CharacterStatus>();
                if (characterStatus != null)
                {
                    characterStatus.estrella++;
                    GameManager.Instance.AgregarEstrella(1);

                    // Desactiva el objeto para evitar colisiones m�ltiples.
                    GetComponent<Collider>().enabled = false;
                    GetComponent<Renderer>().enabled = false;

                    // Aumenta el puntaje
                    //GameManager.Instance.AgregarPuntos(10);

                    // Inicia la corrutina para cargar la escena de victoria despu�s de que el sonido termine
                    StartCoroutine(LoadWinSceneAfterSound());
                }
                else
                {
                    Debug.LogError("No se encontr� el componente CharacterStatus en el jugador.", this);
                }
            }
            else
            {
                Debug.LogError("Intento de reproducir un clip de audio nulo en WinTrigger.", this);
            }
        }
    }

    IEnumerator LoadWinSceneAfterSound()
    {
        // Espera a que el clip de audio termine
        yield return new WaitForSeconds(audioSource.clip.length);

        // Carga la escena de victoria
        SceneManager.LoadScene(winSceneName);
    }
}
