using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaPlayer : MonoBehaviour
{
    public int vidas = 3;
    public bool venceu = false;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            vidas--;

            if (vidas <= 0)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        SceneManager.LoadScene("Derrota");
    }
}
