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
            PlayerPrefs.SetInt("Perdeu", 1); // salva que perdeu
            PlayerPrefs.Save();

            SceneManager.LoadScene("Inicio"); // tela inicial = game over
        }
        else
        {
            venceu = true;
            SceneManager.LoadScene("Vitoria");
        }
    }

    public void VoltarParaInicio(float delay = 0f)
    {
        if (delay > 0)
            Invoke("CarregarInicio", delay);
        else
            CarregarInicio();
    }

    private void CarregarInicio()
    {
        SceneManager.LoadScene("Inicio");
    }

    public void Resetar()
    {
        score = 0;
        venceu = false;
        perdeu = false;
    }

    
}

public class FimDaFase : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.ChecarVitoria();
        }
    }
}
