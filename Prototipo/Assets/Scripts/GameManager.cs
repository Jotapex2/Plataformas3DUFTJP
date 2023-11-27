using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int Score { get; private set; }
    public int Monedas { get; private set; }
    public int Estrellas { get; private set; }

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI monedaText;
    public TextMeshProUGUI estrellaText;

    void Start()
    {
        UpdateUIReferences();
        UpdateUI();
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUIReferences();
        UpdateUI();
    }

    void UpdateUIReferences()
    {
        // Aquí debes buscar y asignar las referencias de UI
        // Asegúrate de que los nombres coincidan con los de tus objetos de UI en la escena
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        monedaText = GameObject.Find("MonedaText")?.GetComponent<TextMeshProUGUI>();
        estrellaText = GameObject.Find("EstrellaText")?.GetComponent<TextMeshProUGUI>();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + Score.ToString();
        if (monedaText != null)
            monedaText.text = Monedas.ToString();
        if (estrellaText != null)
            estrellaText.text = Estrellas.ToString();
    }

    public void AgregarMoneda(int cantidad)
    {
        Monedas += cantidad;
        if (monedaText != null)
            monedaText.text = Monedas.ToString();
    }

    public void AgregarEstrella(int cantidad)
    {
        Estrellas += cantidad;
        if (estrellaText != null)
            estrellaText.text = Estrellas.ToString();
    }

    // Aquí puedes agregar otros métodos, como AgregarPuntos, etc.
}
