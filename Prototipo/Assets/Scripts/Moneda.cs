using UnityEngine;

public class Moneda : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip soundClip; // Arrastra tu clip de audio aqu� a trav�s del Inspector

    void Start()
    {
        // Obtiene el componente AudioSource anexado al mismo objeto que este script.
        audioSource = GetComponent<AudioSource>();
        // Asigna el clip de audio al AudioSource si no ha sido asignado previamente.
        if (audioSource.clip == null && soundClip != null)
        {
            audioSource.clip = soundClip;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproduce el sonido de la moneda si el clip de audio ha sido asignado.
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
            else
            {
                Debug.LogError("Intento de reproducir un clip de audio nulo en Moneda.");
            }

            // Aumenta el puntaje.
            GameManager.Instance.AgregarPuntos(10);

            // Desactiva el objeto moneda para que no pueda ser recolectado de nuevo mientras el sonido est� reproduci�ndose.
            // Esto tambi�n asegura que la moneda desaparezca si el sonido es m�s corto que el tiempo de desactivaci�n.
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;

            // Opcional: destruir el objeto moneda despu�s de que el sonido se haya reproducido completamente.
            // Esto previene que el AudioSource se destruya antes de que el sonido termine.
            Destroy(gameObject, audioSource.clip != null ? audioSource.clip.length : 0f);
        }
    }
}
