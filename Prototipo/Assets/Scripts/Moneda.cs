using UnityEngine;

public class Moneda : MonoBehaviour
{
    public AudioClip soundClip; // Arrastra tu clip de audio aqu� a trav�s del Inspector
    public GameObject particleSystemPrefab; // Arrastra tu prefab de sistema de part�culas aqu� a trav�s del Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No se encontr� AudioSource. Se agregar� uno autom�ticamente.", this);
            audioSource = gameObject.AddComponent<AudioSource>();
        }

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
            if (audioSource.clip != null)
            {
                audioSource.Play();

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

                GetComponent<Collider>().enabled = false;
                GetComponent<Renderer>().enabled = false;

                if (particleSystemPrefab != null)
                {
                    Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Prefab del sistema de part�culas no asignado.", this);
                }

                Destroy(gameObject, audioSource.clip.length);
            }
            else
            {
                Debug.LogError("Intento de reproducir un clip de audio nulo en Moneda.", this);
            }
        }
    }
}
