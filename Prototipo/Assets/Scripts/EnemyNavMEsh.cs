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
    }
}