using UnityEngine;
using UnityEngine.SceneManagement;

public class Derrota : MonoBehaviour
{
    public void TentarNovamente()
    {
        Destroy(GameManager.instance.gameObject);
        SceneManager.LoadScene("Fase1"); 
    }

}
