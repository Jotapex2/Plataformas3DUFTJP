using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{
    public NavMeshAgent agent;

    private Transform player;

    void Start()
    {
        // Aseg�rate de que est�s utilizando FindObjectOfType correctamente.
        CharacterStatus characterStatus = FindObjectOfType<CharacterStatus>();
        if (characterStatus != null)
        {
            player = characterStatus.transform;
        }
        else
        {
            Debug.LogError("No se encontr� un objeto CharacterStatus en la escena.");
        }
    }

    void Update()
    {
        // Comprueba que el jugador y el agente existen antes de intentar mover al agente.
        if (player != null && agent != null)
        {
            // Aseg�rate de que el agente est� sobre el NavMesh antes de establecer el destino.
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Debug.LogWarning("El NavMeshAgent no est� en el NavMesh.", this);
            }

            // Mira hacia la c�mara manteniendo su orientaci�n hacia adelante
            LookAtCamera();
        }
    }

    private void LookAtCamera()
    {
        if (Camera.main != null)
        {
            Transform cameraTransform = Camera.main.transform;
            Vector3 directionToCamera = cameraTransform.position - transform.position;
            directionToCamera.y = 0; // Ignora la diferencia de altura
            Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajusta el 5f para controlar la velocidad de rotaci�n
        }
        else
        {
            Debug.LogWarning("No se encontr� la c�mara principal.", this);
        }
    }
}
