using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Vencedor : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    public float delayAntesDeVoltar = 3f;

        void Start()
        {
            GameManager.instance.VoltarParaInicio(3f); // espera 3 segundos
        }


}
