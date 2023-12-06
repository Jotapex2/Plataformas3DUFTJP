using UnityEngine;

public class Enemigo1_Script : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public AudioClip deathSound;
    public AudioClip hitSound;
    public GameObject particleSystemPrefab; // Prefab del sistema de partículas
    public GameObject monedaPrefab; // Prefab de la moneda
    public TrailRenderer trailRenderer;

    private void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        if (trailRenderer == null)
            trailRenderer = GetComponent<TrailRenderer>();

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
        SpawnParticleSystem();
        Destroy(gameObject, 0.5f);
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
    void SpawnParticleSystem()
    {
        // Generar el sistema de partículas
        if (particleSystemPrefab != null)
        {
            Instantiate(particleSystemPrefab, transform.position, Quaternion.identity);
        }
    }
}

