using UnityEngine;

public class Movecamera : MonoBehaviour
{
       public GameObject objeto;
    private Vector3 offset;

    void Start()
    {
        // calcula a distância inicial entre a câmera e o objeto
        offset = transform.position - objeto.transform.position;
    }

    void LateUpdate()
    {
        // mantém a câmera seguindo o objeto
        Vector3 novaPosicao = objeto.transform.position + offset;

        // trava o eixo Z para não mexer na profundidade
        novaPosicao.z = transform.position.z;

        transform.position = novaPosicao;
    }
}
