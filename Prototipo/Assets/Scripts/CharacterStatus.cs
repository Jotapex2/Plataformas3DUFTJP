using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour, IDamageable
{
    public int health = 5;
    public int maxHealth = 5;
    public int moneda = 0; // Asume que se actualizar� en otro lugar del c�digo
    public int estrella = 0; // Asume que se actualizar� en otro lugar del c�digo

    public Transform lastRespawnPoint;
    public Renderer characterRenderer; // Asignar en el Inspector
    public float invulnerabilityDuration = 15f;
    public UnityEvent onHealthChanged; // Aseg�rate de configurar los suscriptores en el Inspector

    private bool isInvulnerable = false;

    private void Start()
    {
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
            // Desactivar temporalmente el CharacterController si est� presente
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
            Debug.LogError("lastRespawnPoint no est� asignado en el personaje.");
        }
    }

}

public interface IDamageable
{
    void AddDamage(int damage);
    void Heal(int heal);
    bool IsDead();
}
