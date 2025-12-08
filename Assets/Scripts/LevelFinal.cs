using UnityEngine;

public class LevelFinal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.instance.FinalDaFase();
    }
}
