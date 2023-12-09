using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealthZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        ApplyDamage(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ApplyDamage(collision.collider);
    }

    private void ApplyDamage(Collider collider)
    {
        var damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.AddDamage(100);
        }
    }
}