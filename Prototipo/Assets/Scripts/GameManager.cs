using UnityEngine;
using TMPro; // Asegúrate de incluir este namespace si usas TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public int Monedas { get; private set; }
    public int Estrellas { get; private set; }

    public TextMeshProUGUI scoreText; // Referencia al componente de texto de la UI para el puntaje
    public TextMeshProUGUI monedaText; // Referencia al componente de texto de la UI para las monedas
    public TextMeshProUGUI estrellaText; // Referencia al componente de texto de la UI para las estrellas

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

    public void AgregarMoneda(int cantidad)
    {
        Monedas += cantidad;
        // Actualiza la interfaz de usuario con la nueva cantidad de monedas
        if (monedaText != null)
            monedaText.text = "Monedas: " + Monedas.ToString();
    }

    public void AgregarEstrella(int cantidad)
    {
        Estrellas += cantidad;
        // Actualiza la interfaz de usuario con la nueva cantidad de estrellas
        if (estrellaText != null)
            estrellaText.text = "Estrellas: " + Estrellas.ToString();
    }
}
