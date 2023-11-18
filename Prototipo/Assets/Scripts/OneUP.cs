using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Obtén el componente CharacterStatus del jugador
        var characterStatus = other.GetComponent<CharacterStatus>();

        if (characterStatus != null)
        {
            // Verifica si la salud del jugador es menor a 5
            if (characterStatus.health < 5)
            {
                // Si es así, cura al jugador y destruye el objeto OneUP
                characterStatus.Heal(1);
                Destroy(gameObject);
            }
        }
    }
}
