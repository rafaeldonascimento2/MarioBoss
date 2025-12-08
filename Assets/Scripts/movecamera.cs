using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public GameObject objeto;
    private Vector3 offset;

    void Start()
    {
        // Se o objeto não existir ao iniciar, evita erro
        if (objeto != null)
            offset = transform.position - objeto.transform.position;
    }

    void LateUpdate()
    {
        // SE O OBJETO MORREU, NÃO FAZ MAIS NADA
        if (objeto == null) return;

        // mantém a câmera seguindo o objeto
        Vector3 novaPosicao = objeto.transform.position + offset;

        // trava o eixo Z para não mexer na profundidade
        novaPosicao.z = transform.position.z;

        transform.position = novaPosicao;
    }
}
