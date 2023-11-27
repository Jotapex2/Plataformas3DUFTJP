using UnityEngine;

public class CameraSwing : MonoBehaviour
{
    public float angle = 30.0f; // El ángulo máximo de oscilación
    public float speed = 2.0f; // La velocidad de la oscilación

    private float startAngle; // Ángulo inicial de la cámara

    void Start()
    {
        startAngle = transform.eulerAngles.y; // Guarda el ángulo inicial de la cámara
    }

    void Update()
    {
        // Calcula el nuevo ángulo
        float angleDelta = Mathf.Sin(Time.time * speed) * angle;
        float newAngle = startAngle + angleDelta;

        // Aplica el nuevo ángulo a la cámara
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newAngle, transform.eulerAngles.z);
    }
}
