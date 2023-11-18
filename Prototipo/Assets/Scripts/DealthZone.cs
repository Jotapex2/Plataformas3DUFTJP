using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealthZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var damageble = other.GetComponent<IDamageable>();

        if (damageble != null)
        {
            damageble.AddDamage(100);
        }
    }
}
