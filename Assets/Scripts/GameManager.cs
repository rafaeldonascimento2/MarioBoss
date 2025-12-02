using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Vidas")]
    public int vidasMax = 3;
    public int vidas; // vai ser carregada do PlayerPrefs
    public TextMeshProUGUI vidasText;

    [Header("Placar")]
    public int score = 0;
    public int killsPorVida = 5;
    public TextMeshProUGUI scoreText;

    [Header("Estados")]
    public bool venceu = false;
    public bool perdeu = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre as cenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Carrega vidas salvas ou inicia com o máximo
        vidas = PlayerPrefs.GetInt("Vidas", vidasMax);
        AtualizarUI();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // tenta achar textos de novo quando uma cena é carregada
        if (scoreText == null)
            scoreText = GameObject.FindWithTag("ScoreText")?.GetComponent<TextMeshProUGUI>();

        if (vidasText == null)
            vidasText = GameObject.FindWithTag("VidasText")?.GetComponent<TextMeshProUGUI>();

        AtualizarUI();
    }

    public void AddScore(int amount)
    {
        score += amount;
        AtualizarUI();
    }

    void AtualizarUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString("00") + "/" + killsPorVida.ToString("00");

        if (vidasText != null)
            vidasText.text = "x " + vidas.ToString();
    }

    public void PlayerMorreu()
    {
        vidas--;
        PlayerPrefs.SetInt("Vidas", vidas);
        PlayerPrefs.Save();

        if (vidas <= 0)
        {
            perdeu = true;
            SceneManager.LoadScene("Derrota");
            return;
        }

        // ainda tem vida: reinicia fase
        score = 0;
        SceneManager.LoadScene("Fase1");
    }


    public void FimDaFaseAlcancado()
    {
        if (score >= killsPorVida)
        {
            VencerFase();
        }
        else
        {
            PlayerMorreu();
        }
    }


    public void VencerFase()
    {
        venceu = true;
        PlayerPrefs.SetInt("VidasRestantes", vidas);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Vitoria");
    }


    public int CalcularEstrelas()
    {
        if (vidas == 3) return 3;
        if (vidas == 2) return 2;
        if (vidas == 1) return 1;
        return 0;
    }

    // Chama isso quando voltar para o menu e quiser resetar tudo
    public void ResetarJogo()
    {
        vidas = vidasMax;
        score = 0;
        venceu = false;
        perdeu = false;

        PlayerPrefs.SetInt("Vidas", vidas);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Inicio");
    }
}
