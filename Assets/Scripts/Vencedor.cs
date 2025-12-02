using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Vencedor : MonoBehaviour
{
    public float delayAntesDeVoltar = 3f;

    void Start()
    {
        Invoke(nameof(VoltarParaInicio), delayAntesDeVoltar);
    }

    void VoltarParaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
