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
    TextMeshProUGUI pontosText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject, 0.05f);
        }
    }


        void Start()
    {
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Procura Texts na cena atual pelo NOME
    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        // Quando volta para a tela inicial, zera o jogo
        if (cena.name == "Inicio")
        {
            vidas = vidasMax;
            score = 0;
        }

        // Procura Texts na cena atual pelo NOME
        var scoreObj = GameObject.Find("pontosText");
        pontosText = scoreObj ? scoreObj.GetComponent<TextMeshProUGUI>() : null;

        var vidasObj = GameObject.Find("vidasText");
        vidasText = vidasObj ? vidasObj.GetComponent<TextMeshProUGUI>() : null;

        AtualizaPontosUI();
        AtualizaVidaUI();
    }

    void AtualizaPontosUI()
    {
        if (pontosText)
            pontosText.text = score.ToString("00") + "/" + totalEnemies.ToString("00");
        
    }

    void AtualizaVidaUI()
    {
        if (vidasText)
            vidasText.text = vidas.ToString("00") + "/" + vidasMax.ToString("00");
    }

    // ========= SCORE =========
    public void AddScore(int amount)
    {
        score += amount;
        AtualizaPontosUI();
    }

    // ========= VIDAS =========
    public void PerderVida()
    {
        vidas--;

        if (vidas < 0)
            vidas = 0;

        AtualizaVidaUI();

        if (vidas <= 0)
        {
            // Derrota final
            SceneManager.LoadSceneAsync("Derrota");
        }
        else
        {
            // Perdeu 1 vida, volta para a fase
            SceneManager.LoadSceneAsync("Fase1");
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
            Debug.Log("Chamando Vitoria com VIDAS = " + vidas);
            PlayerPrefs.SetInt("vidasVitoria", vidas);
            PlayerPrefs.Save();
            // Vitória
            SceneManager.LoadSceneAsync("Vitoria");
        }
    }

    public void PerderVidaSemResetarCena()
{
    vidas--;

    if (vidas <= 0)
    {
        SceneManager.LoadSceneAsync("Derrota");
        return;
    }

    AtualizaVidaUI();
}

}
