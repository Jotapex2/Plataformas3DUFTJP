using UnityEngine;

public class Moneda : MonoBehaviour
{
    public AudioClip soundClip; // Arrastra tu clip de audio aquí a través del Inspector
    public GameObject particleSystemPrefab; // Arrastra tu prefab de sistema de partículas aquí a través del Inspector

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No se encontró AudioSource. Se agregará uno automáticamente.", this);
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
                    Debug.LogError("No se encontró el componente CharacterStatus en el jugador.", this);
                }

                GetComponent<Collider>().enabled = false;
                GetComponent<Renderer>().enabled = false;

                if (particleSystemPrefab != null)
                {
                    Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogWarning("Prefab del sistema de partículas no asignado.", this);
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
