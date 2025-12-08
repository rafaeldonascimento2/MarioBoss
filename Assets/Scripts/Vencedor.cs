using UnityEngine;

public class Vencedor : MonoBehaviour
{
    public GameObject[] estrelas;

    void Start()
    {
        int estrelasGanhas = PlayerPrefs.GetInt("estrelasVitoria", 0);

        Debug.Log("Estrelas recebidas: " + estrelasGanhas);

        // Garante que o número não passe do tamanho do array
        estrelasGanhas = Mathf.Clamp(estrelasGanhas, 0, estrelas.Length);

        // Liga apenas a quantidade correta de estrelas
        for (int i = 0; i < estrelas.Length; i++)
        {
            estrelas[i].SetActive(i < estrelasGanhas);
        }
    }
}
