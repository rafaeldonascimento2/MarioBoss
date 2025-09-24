using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Vencedor : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject estrela1;
    public GameObject estrela2;
    public GameObject estrela3;

    public float delayAntesDeVoltar = 5f;

    void Start()
    {
        // dados do GameManager
        int score = GameManager.instance.score;
        int estrelas = GameManager.instance.estrelas;

        scoreText.text = "score: " + score;

        estrela1.SetActive(estrelas >= 1);
        estrela2.SetActive(estrelas >= 2);
        estrela3.SetActive(estrelas >= 3);
        Invoke("VoltarMenu", delayAntesDeVoltar);
    }

    void VoltarMenu()
    {
        GameManager.instance.Resetar();
        SceneManager.LoadScene("Inicio");
    }
}
