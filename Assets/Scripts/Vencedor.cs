using UnityEngine;
using UnityEngine.SceneManagement;

public class Vencedor : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(Voltar), 3f);
    }

    void Voltar()
    {
        SceneManager.LoadScene("Inicio");
    }
}
