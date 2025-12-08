using UnityEngine;

public class Movecamera : MonoBehaviour
{
    public GameObject objeto;
    private Vector3 offset;

    void Start()
    {
        if (objeto != null)
            offset = transform.position - objeto.transform.position;
    }

    void LateUpdate()
    {
        // se morreu:
        if (objeto == null) return;

        Vector3 novaPosicao = objeto.transform.position + offset;
        // trava o eixo Z para não mexer na profundidade
        novaPosicao.z = transform.position.z;
        transform.position = novaPosicao;
    }
}
