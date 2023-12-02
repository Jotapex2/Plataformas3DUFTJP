using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2Die : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip deathSound;
    public GameObject monedaPrefab; // Prefab de la moneda
    public GameObject particleSystemPrefab; // Prefab del sistema de part�culas

    private void Start()
    {
        // Asegurarse de que el animator y el audioSource est�n asignados
        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar colisi�n con el jugador
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
        // Activar animaci�n de muerte, sonido, generar moneda y sistema de part�culas
        animator.SetTrigger("Die");
        PlayDeathSound();
        SpawnMoneda();
        SpawnParticleSystem();
        Destroy(gameObject, 2f); // Destruir el objeto despu�s de 2 segundos
    }

    void PlayDeathSound()
    {
        // Reproducir sonido de muerte
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    void SpawnMoneda()
    {
        // Generar una moneda
        if (monedaPrefab != null)
        {
            Instantiate(monedaPrefab, transform.position, Quaternion.identity);
        }
    }

    void SpawnParticleSystem()
    {
        // Generar el sistema de part�culas
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        }
    }
}
