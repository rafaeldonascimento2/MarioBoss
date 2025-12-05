using UnityEngine;

public class Goomba : MonoBehaviour
{
    Rigidbody2D rbGoomba;

    [SerializeField] float speed = 2f;
    [SerializeField] Transform point1, point2;
    [SerializeField] LayerMask layer;
    [SerializeField] bool isColliding;

    [Header("Stomp")]
    [SerializeField] float stompBoost = 7f;       // impulso dado ao jogador ao pisar
    [SerializeField] float headThreshold = 0.1f;  // margem acima do goomba

    Animator aniGoomba;
    BoxCollider2D colliderGoomba;

    void Awake()
    {
        rbGoomba = GetComponent<Rigidbody2D>();
        aniGoomba = GetComponent<Animator>();
        colliderGoomba = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        rbGoomba.linearVelocity = new Vector2(speed, rbGoomba.linearVelocity.y);

        isColliding = Physics2D.Linecast(point1.position, point2.position, layer);
        Debug.DrawLine(point1.position, point2.position, isColliding ? Color.green : Color.blue);

        if (isColliding)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Rigidbody2D rbPlayer = other.GetComponent<Rigidbody2D>();
        if (rbPlayer == null) return;

        bool isStomp =
            rbPlayer.linearVelocity.y <= 0f &&
            other.transform.position.y > transform.position.y + headThreshold;

        if (isStomp)
        {
            rbPlayer.linearVelocity = Vector2.zero;
            rbPlayer.AddForce(new Vector2(0f, stompBoost), ForceMode2D.Impulse);

            if (aniGoomba) aniGoomba.SetTrigger("Death");
            speed = 0f;
            if (colliderGoomba) colliderGoomba.enabled = false;

            GameManager.instance.AddScore(1);

            Destroy(gameObject, 0.3f);
        }
        else
        {
            var player = other.GetComponent<PlayerMovement>();
            if (player != null) player.Death();
        }
    }
}
