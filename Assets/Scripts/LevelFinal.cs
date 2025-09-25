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
            PlayerPrefs.SetInt("Pontos", pontos);    

            SceneManager.LoadScene("Vitoria");
        }
    }
}

