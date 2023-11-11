using UnityEngine;

public class PlayerPlatformAttachment : MonoBehaviour
{
    private Transform currentPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MovingPlatform"))
        {
            // Haz al jugador hijo de la plataforma
            this.transform.parent = other.transform;
            currentPlatform = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == currentPlatform)
        {
            // Cuando el jugador sale de la plataforma, elimina el parentesco
            this.transform.parent = null;
            currentPlatform = null;
        }
    }
}
