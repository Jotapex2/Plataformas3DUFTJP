using UnityEngine;

public class PlayerPush : MonoBehaviour
{
    public float pushPower = 2.0f; // Puedes ajustar esta fuerza según sea necesario

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // Si no hay Rigidbody o el Rigidbody es kinemático, no hacer nada
        if (body == null || body.isKinematic)
        {
            return;
        }

        // No empujar objetos debajo del jugador (e.g., el suelo)
        if (hit.moveDirection.y < -0.3f)
        {
            return;
        }

        // Calcula la dirección y la fuerza del empuje
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.AddForce(pushDir * pushPower, ForceMode.Impulse);
    }
}
