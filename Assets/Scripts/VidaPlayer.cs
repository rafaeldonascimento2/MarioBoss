using System.Collections;
using UnityEngine;
using TMPro;

public class VidaPlayer : MonoBehaviour
{
    public int vidas = 3;                
    public int maxVidas = 3;

    public float tempoInvencivel = 1f;
    bool invencivel = false;

    SpriteRenderer sprite;
    PlayerMovement movimentos;
    TextMeshProUGUI vidasText;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        movimentos = GetComponent<PlayerMovement>();

        // procura automaticamente o texto no Canvas
        vidasText = GameObject.Find("vidasText").GetComponent<TextMeshProUGUI>();
        AtualizarHUD();
    }

    public void TomarDano()
    {
        if (invencivel) return;

        vidas--;
        AtualizarHUD();

        Debug.Log("Mario tomou dano! Vidas = " + vidas);

        if (vidas <= 0)
        {
            // impede qualquer trigger ou colisão extra
            GetComponent<Collider2D>().enabled = false;
            GetComponent<Rigidbody2D>().simulated = false;
            movimentos.Death();
            return;
        }


        StartCoroutine(Invencibilidade());
    }

    void AtualizarHUD()
    {
        if (vidasText != null)
        {
            vidasText.text = vidas.ToString("00") + "/" + maxVidas.ToString("00");
        }
    }

    IEnumerator Invencibilidade()
    {
        invencivel = true;

        for (int i = 0; i < 6; i++)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(tempoInvencivel / 6f);
        }

        sprite.enabled = true;
        invencivel = false;
    }


}
