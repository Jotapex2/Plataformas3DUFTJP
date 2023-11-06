using UnityEngine;
using UnityEngine.AI;

public class EnemigoTriangulo : MonoBehaviour
{
    private NavMeshAgent agente;
    public Transform jugador;
    public float rangoPersecucion = 10f;
    public LayerMask queEsSuelo;
    public float distanciaDeSeguridad = 1f;

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Detectar el suelo para evitar ca�das
        if (!DetectarSuelo())
        {
            // Invertir direcci�n o tomar una acci�n para evitar la ca�da
        }

        // Persecuci�n del jugador
        float distanciaAlJugador = Vector3.Distance(jugador.position, transform.position);
        if (distanciaAlJugador < rangoPersecucion)
        {
            // Perseguir al jugador
            agente.SetDestination(jugador.position);
        }
        else
        {
            // Volver a comportamiento normal, como patrullaje
        }
    }

    bool DetectarSuelo()
    {
        RaycastHit hit;
        // Realiza un raycast hacia abajo desde el enemigo
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distanciaDeSeguridad, queEsSuelo))
        {
            return true; // Suelo detectado
        }
        return false; // Borde detectado, no hay suelo
    }
}
