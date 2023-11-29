using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo2Die : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;

    public AudioClip deathSound;
    public AudioClip hitSound;

    public GameObject monedaPrefab; // Prefab de la moneda

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
                    PlayHitSound();
                }
            }
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        PlayDeathSound();
        SpawnMoneda();
        Destroy(gameObject, 2f);
    }

    void PlayDeathSound()
    {
        if (deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    void PlayHitSound()
    {
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }

    void SpawnMoneda()
    {
        if (monedaPrefab != null)
        {
            Instantiate(monedaPrefab, transform.position, Quaternion.identity);
        }
    }
}
