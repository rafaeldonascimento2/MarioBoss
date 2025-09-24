using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0; // score
    public int totalEnemies = 10; // total
    public TextMeshProUGUI scoreText;
    public bool venceu = false;
    public bool perdeu = false;
    public int estrelas = 0;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void Start()
    {
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = score.ToString("00") + "/" + totalEnemies.ToString("00");
    }

    public void ChecarFimDeJogo()
    {
        if (score < 5)
        {
            perdeu = true;
            SceneManager.LoadScene("Inicio"); // tela inicial = game over
        }
        else
        {
            venceu = true;
            CalcularEstrelas();
            SceneManager.LoadScene("Vitoria");
        }
    }
    void CalcularEstrelas()
    {
        if (score >= 10)
            estrelas = 3;
        else if (score >= 7)
            estrelas = 2;
        else
            estrelas = 1;
    }

    public void Resetar()
    {
        score = 0;
        venceu = false;
        perdeu = false;
        estrelas = 0;
    }

    
}
