using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneUP : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.Heal(1);
            Destroy(gameObject); // Destruye el objeto HealZone
        }
    }
}
