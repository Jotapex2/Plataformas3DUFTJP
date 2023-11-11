using UnityEngine;
using TMPro; // Asegúrate de incluir este namespace si usas TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public TextMeshProUGUI scoreText; // Referencia al componente de texto de la UI

    void Awake()
    {
        // Configurar el singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AgregarPuntos(int puntos)
    {
        Score += puntos;
        // Actualiza la interfaz de usuario con el nuevo puntaje
        if (scoreText != null)
            scoreText.text = "Score: " + Score.ToString();
    }
}
