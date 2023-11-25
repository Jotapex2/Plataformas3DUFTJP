using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo1_Script : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Movimiento playerMovement = other.GetComponent<Movimiento>();
            if (playerMovement != null && playerMovement.IsFalling())
            {
                Die();
            }
            else
            {
                var damageable = other.GetComponent<IDamageable>();

                if (damageable != null)
                {
                    damageable.AddDamage(1);
                }
            }
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 2f); // Ajusta según la duración de la animación de muerte
    }
}
