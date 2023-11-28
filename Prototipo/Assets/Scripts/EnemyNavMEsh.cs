using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMEsh : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<CharacterStatus>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
        // Hacer que el enemigo mire hacia la cámara manteniendo su orientación hacia adelante
        Transform cameraTransform = Camera.main.transform;
        Vector3 directionToCamera = cameraTransform.position - transform.position;
        directionToCamera.y = 0; // Ignora la diferencia de altura
        Quaternion lookRotation = Quaternion.LookRotation(directionToCamera);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajusta el 5f para controlar la velocidad de rotación
    }
}