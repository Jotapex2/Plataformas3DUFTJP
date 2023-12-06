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

    public GameObject menuPausa; // Referencia al objeto del menú de pausa en la escena
    public GameObject jugador; // Referencia al jugador
    public CharacterStatus characterStatus; // Referencia al estado del personaje

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

    void Start()
    {
        CargarProgreso();
        UpdateUIReferences();
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausa();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateUIReferences();
        UpdateUI();
    }

    void UpdateUIReferences()
    {
        scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();
        monedaText = GameObject.Find("MonedaText")?.GetComponent<TextMeshProUGUI>();
        estrellaText = GameObject.Find("EstrellaText")?.GetComponent<TextMeshProUGUI>();
    }

    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = Score.ToString();
        if (monedaText != null)
            monedaText.text = Monedas.ToString();
        if (estrellaText != null)
            estrellaText.text = Estrellas.ToString();
    }

    void TogglePausa()
    {
        if (menuPausa != null)
        {
            menuPausa.SetActive(!menuPausa.activeSelf);

            if (menuPausa.activeSelf)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void AgregarMoneda(int cantidad)
    {
        Monedas += cantidad;
        UpdateUI();
    }

    public void AgregarEstrella(int cantidad)
    {
        Estrellas += cantidad;
        UpdateUI();
    }

    public void GuardarProgreso()
    {
        if (jugador != null)
        {
            Vector3 posicionJugador = jugador.transform.position;
            PlayerPrefs.SetFloat("JugadorPosX", posicionJugador.x);
            PlayerPrefs.SetFloat("JugadorPosY", posicionJugador.y);
            PlayerPrefs.SetFloat("JugadorPosZ", posicionJugador.z);
        }

        if (characterStatus != null)
        {
            PlayerPrefs.SetFloat("health", characterStatus.health);
        }

        PlayerPrefs.SetInt("Score", Score);
        PlayerPrefs.SetInt("Monedas", Monedas);
        PlayerPrefs.SetInt("Estrellas", Estrellas);
        PlayerPrefs.Save();

        DesactivarMenuPausa();
    }

    public void CargarProgreso()
    {
        if (jugador != null)
        {
            float x = PlayerPrefs.GetFloat("JugadorPosX", jugador.transform.position.x);
            float y = PlayerPrefs.GetFloat("JugadorPosY", jugador.transform.position.y);
            float z = PlayerPrefs.GetFloat("JugadorPosZ", jugador.transform.position.z);
            Vector3 posicionCargada = new Vector3(x, y, z);

            Movimiento movimientoJugador = jugador.GetComponent<Movimiento>();
            if (movimientoJugador != null)
            {
                movimientoJugador.EstablecerPosicion(posicionCargada);
            }
            if (characterStatus != null)
            {
                characterStatus.HacerInvulnerable();
            }
        }

        if (characterStatus != null)
        {
            characterStatus.health = (int)PlayerPrefs.GetFloat("health", characterStatus.health);
        }

        Score = PlayerPrefs.GetInt("Score", 0);
        Monedas = PlayerPrefs.GetInt("Monedas", 0);
        Estrellas = PlayerPrefs.GetInt("Estrellas", 0);
        UpdateUI();

        DesactivarMenuPausa();
    }

    private void DesactivarMenuPausa()
    {
        if (menuPausa != null && menuPausa.activeSelf)
        {
            menuPausa.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Aquí puedes agregar otros métodos, como AgregarPuntos, etc.
}
