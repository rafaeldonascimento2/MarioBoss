using UnityEngine;
using UnityEngine.SceneManagement;

public class Derrota : MonoBehaviour
{
    public void JogarNovamente()
    {
        // Reinicia tudo do zero
        GameManager.instance.vidas = GameManager.instance.vidasMax;
        GameManager.instance.score = 0;

        SceneManager.LoadScene("Fase1");
    }

    public void VoltarParaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
