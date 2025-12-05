using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Configuração")]
    public int vidasMax = 3;
    public int totalEnemies = 10;

    [Header("Estado (só para visualizar)")]
    public int vidas;
    public int score;

    TextMeshProUGUI vidasText;
    TextMeshProUGUI scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);   // fica vivo entre as cenas
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

        void Start()
    {
        AtualizaUI();
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Chamado toda vez que uma cena é carregada
    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Quando volta para a tela inicial, zera o jogo
        if (cena.name == "Inicio")
        {
            vidas = vidasMax;
            score = 0;
        }

        // Procura Texts na cena atual pelo NOME
        var scoreObj = GameObject.Find("ScoreText");
        scoreText = scoreObj ? scoreObj.GetComponent<TextMeshProUGUI>() : null;

        var vidasObj = GameObject.Find("VidasText");
        vidasText = vidasObj ? vidasObj.GetComponent<TextMeshProUGUI>() : null;

        AtualizaUI();
    }

    void AtualizaUI()
    {
        if (scoreText)
            scoreText.text = score.ToString("00") + "/" + totalEnemies.ToString("00");

        if (vidasText)
            vidasText.text = "Vidas: " + vidas.ToString();
    }

    // ========= SCORE =========
    public void AddScore(int amount)
    {
        score += amount;
        AtualizaUI();
    }

    // ========= VIDAS =========
    public void PerderVida()
    {
        vidas--;

        if (vidas < 0)
            vidas = 0;

        AtualizaUI();

        if (vidas <= 0)
        {
            // Derrota final
            SceneManager.LoadScene("Derrota");
        }
        else
        {
            // Perdeu 1 vida, volta para a fase
            SceneManager.LoadScene("Fase1");
        }
    }

    // ========= FINAL DA FASE =========
    public void FinalDaFase()
    {
        if (score < 5)
        {
            // Não fez pontos suficientes → perde vida
            PerderVida();
        }
        else
        {
            // Vitória
            SceneManager.LoadScene("Vitoria");
        }
    }
}
