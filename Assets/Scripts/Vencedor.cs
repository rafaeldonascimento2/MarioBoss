using UnityEngine;
using UnityEngine.SceneManagement;

public class Vencedor : MonoBehaviour
{
    public GameObject[] estrelas;

    void Start()
    {
        int estrelasGanhas = PlayerPrefs.GetInt("estrelasVitoria", 0);
        estrelasGanhas = Mathf.Clamp(estrelasGanhas, 0, estrelas.Length);

        // quantidade de estrelas
        for (int i = 0; i < estrelas.Length; i++)
        {
            estrelas[i].SetActive(i < estrelasGanhas);
        }
          StartCoroutine(VoltarParaInicio());
    }

    private System.Collections.IEnumerator VoltarParaInicio()
    {
        yield return new WaitForSeconds(5f);
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Inicio");
    }
}
