using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int pontos = GameManager.instance.score;

        if (pontos < 5)
        {
            SceneManager.LoadScene("Inicio");
        }
        else
        {
            // conta os pontos
            int estrelas = 1;
            if (pontos >= 10) estrelas = 3;
            else if (pontos >= 7) estrelas = 2;

            PlayerPrefs.SetInt("Estrelas", estrelas);
            PlayerPrefs.SetInt("Pontos", pontos);    

            SceneManager.LoadScene("Vitoria");
        }
    }
}

