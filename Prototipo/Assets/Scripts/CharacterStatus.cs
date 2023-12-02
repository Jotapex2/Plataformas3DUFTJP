using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour, IDamageable
{
    public int health = 5;
    public int maxHealth = 5;
    public int moneda = 0; // Asume que se actualizará en otro lugar del código
    public int estrella = 0; // Asume que se actualizará en otro lugar del código

    public Transform lastRespawnPoint;
    public Renderer characterRenderer; // Asignar en el Inspector
    public float invulnerabilityDuration = 15f;
    public UnityEvent onHealthChanged; // Asegúrate de configurar los suscriptores en el Inspector
    public AudioClip hitSound;

    private AudioSource audioSource; // Fuente de audio para reproducir sonidos
    private bool isInvulnerable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Obtener el componente AudioSource
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource no encontrado en el objeto, se requiere para reproducir sonidos.");
        }

        if (characterRenderer != null)
        {
            characterRenderer.enabled = true;
        }
    }

    public void AddDamage(int damage)
    {
        if (isInvulnerable || IsDead())
            return;

        health -= Mathf.Max(damage, 0);

        // Invoca el evento para notificar a los suscriptores del cambio de salud
        onHealthChanged.Invoke();

        if (IsDead())
        {
            Respawn();
        }
        else
        {
            StartCoroutine(BecomeInvulnerable());
            PlayHitSound();
        }
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void Heal(int heal)
    {
        if (!IsDead())
        {
            health += Mathf.Max(heal, 0);
            health = Mathf.Min(health, maxHealth);
            // Notifica a los suscriptores que la salud ha cambiado
            onHealthChanged.Invoke();
        }
    }

    private IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;

        if (characterRenderer != null)
        {
            var endTime = Time.time + invulnerabilityDuration;
            while (Time.time < endTime)
            {
                characterRenderer.enabled = !characterRenderer.enabled;
                yield return new WaitForSeconds(0.1f);
            }
            characterRenderer.enabled = true;
        }

        isInvulnerable = false;
    }

    private void Respawn()
    {
        if (lastRespawnPoint != null)
        {
            // Desactivar temporalmente el CharacterController si está presente
            CharacterController controller = GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }

            transform.position = lastRespawnPoint.position;
            health = maxHealth;

            // Reactivar el CharacterController si estaba presente
            if (controller != null)
            {
                controller.enabled = true;
            }

            isInvulnerable = false;
            StopAllCoroutines();
            characterRenderer.enabled = true;
            onHealthChanged.Invoke();
        }
        else
        {
            Debug.LogError("lastRespawnPoint no está asignado en el personaje.");
        }
    }

    private void PlayHitSound()
    {
        if (hitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
    }
}

public interface IDamageable
{
    void AddDamage(int damage);
    void Heal(int heal);
    bool IsDead();
}
