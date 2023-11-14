using UnityEngine;

public class Moneda : MonoBehaviour
{
    public AudioClip soundClip; // Arrastra tu clip de audio aqu� a trav�s del Inspector

    private AudioSource audioSource;

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
        if (other.CompareTag("Player"))
        {
            // Aseg�rate de que el clip de audio no sea nulo antes de intentar reproducirlo.
            if (audioSource.clip != null)
            {
                audioSource.Play();

                // Obtiene el componente CharacterStatus del jugador y aumenta la moneda.
                CharacterStatus characterStatus = other.GetComponent<CharacterStatus>();
                if (characterStatus != null)
                {
                    characterStatus.moneda++;
                    GameManager.Instance.AgregarMoneda(1);
                }
                else
                {
                    Debug.LogError("No se encontr� el componente CharacterStatus en el jugador.", this);
                }

                // Aumenta el puntaje.
                //GameManager.Instance.AgregarPuntos(10);

                // Desactiva el objeto moneda para que no pueda ser recolectado de nuevo mientras el sonido est� reproduci�ndose.
                GetComponent<Collider>().enabled = false;
                GetComponent<Renderer>().enabled = false;

                // Opcional: destruir el objeto moneda despu�s de que el sonido se haya reproducido completamente.
                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Debug.LogError("Intento de reproducir un clip de audio nulo en Moneda.", this);
            }
        }
    }
}
