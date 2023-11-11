using UnityEngine;

public class SecondMovingPlatform : MonoBehaviour
{
    public Transform puntoA;
    public Transform puntoB;
    public float velocidad = 3f;

    private Vector3 destino;

    void Start()
    {
        // Comienza moviéndose hacia el punto B.
        destino = puntoB.position;
    }

    void Update()
    {
        // Mueve la plataforma hacia el destino actual.
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime);

        // Si la plataforma llega al destino (dentro de un pequeño margen de error), cambia el destino.
        if (Vector3.Distance(transform.position, destino) < 0.1f)
        {
            destino = destino == puntoA.position ? puntoB.position : puntoA.position;
        }
    }
}
