using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WinTrigger : MonoBehaviour
{
    public AudioClip soundClip;
    public GameObject particleSystemPrefab;

    private AudioSource audioSource;
    private bool isCollected = false;
    public string winSceneName = "WinScene";

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
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;

            if (particleSystemPrefab != null)
            {
                Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Debug.LogWarning("Prefab del sistema de partículas no asignado.", this);
            }

            if (audioSource.clip != null)
            {
                audioSource.Play();

                CharacterStatus characterStatus = other.GetComponent<CharacterStatus>();
                if (characterStatus != null)
                {
                    characterStatus.estrella++;
                    GameManager.Instance.AgregarEstrella(1);

                    GetComponent<Collider>().enabled = false;
                    GetComponent<Renderer>().enabled = false;

                    StartCoroutine(LoadWinSceneAfterSound());
                }
                else
                {
                    Debug.LogError("No se encontró el componente CharacterStatus en el jugador.", this);
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
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(winSceneName);
    }
}
