using UnityEngine;

public class CameraSwing : MonoBehaviour
{
    public float angle = 30.0f; // El �ngulo m�ximo de oscilaci�n
    public float speed = 2.0f; // La velocidad de la oscilaci�n

    private float startAngle; // �ngulo inicial de la c�mara

    void Start()
    {
        startAngle = transform.eulerAngles.y; // Guarda el �ngulo inicial de la c�mara
    }

    void Update()
    {
        // Calcula el nuevo �ngulo
        float angleDelta = Mathf.Sin(Time.time * speed) * angle;
        float newAngle = startAngle + angleDelta;

        // Aplica el nuevo �ngulo a la c�mara
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, newAngle, transform.eulerAngles.z);
    }
}
