using UnityEngine;

public class Vencedor : MonoBehaviour
{
    public GameObject[] estrelas;
    void Start()
    {
        int vidas = PlayerPrefs.GetInt("vidasVitoria", 0);
        Debug.Log("Vidas recebidas na VictoryScreen: " + vidas);

        vidas = Mathf.Clamp(vidas, 0, estrelas.Length);

        for (int i = 0; i < estrelas.Length; i++)
            estrelas[i].SetActive(i < vidas);
    }

}
