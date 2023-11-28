using UnityEngine;

public class Enemigo1_Script : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource; // Componente AudioSource

    public AudioClip deathSound; // Sonido de muerte
    public AudioClip hitSound; // Sonido cuando el enemigo golpea al jugador

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
        Destroy(gameObject, 2f); // Ajusta según la duración de la animación de muerte
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
}
