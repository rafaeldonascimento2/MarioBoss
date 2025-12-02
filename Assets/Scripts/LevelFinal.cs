using UnityEngine;

public class LevelFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        int pontos = GameManager.instance.score;
        int minKills = GameManager.instance.killsPorVida; 


        if (pontos < minKills)
        {
            // Não matou o suficiente → perde 1 vida e recomeça
            GameManager.instance.PlayerMorreu();
        }
        else
        {
            // Matou o suficiente → vitória
            GameManager.instance.VencerFase();
        }
    }
}
