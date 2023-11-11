using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Image healthBackground;
    public Text healthText;

    private CharacterStatus status;

    void Start()
    {
        status = FindObjectOfType<CharacterStatus>();
        if (status != null)
        {
            // Suscríbete al evento onHealthChanged.
            status.onHealthChanged.AddListener(UpdateHealthUI);
            UpdateHealthUI(); // Actualiza la UI en el inicio para mostrar la salud inicial.
        }
    }

    public void UpdateHealthUI()
    {
        // Actualiza el texto y la imagen de fondo para reflejar la salud actual.
        healthText.text = $"{status.health}";
        healthBackground.fillAmount = (float)status.health / status.maxHealth;
    }
}
