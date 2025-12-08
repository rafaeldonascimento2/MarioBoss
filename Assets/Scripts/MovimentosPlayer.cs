using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;

    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;

    Animator animPlayer;

    bool dead = false;
    CapsuleCollider2D playerCollider;

    void Awake()
    {
        animPlayer = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (dead) return;

        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);

        animPlayer.SetBool("Jump", !inFloor);

        if (Input.GetButtonDown("Jump") && inFloor)
        {
            isJump = true;
        }
        else if (Input.GetButtonUp("Jump") && rbPlayer.linearVelocity.y > 0)
        {
            rbPlayer.linearVelocity =
                new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.5f);
        }
    }

    void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }

    void Move()
    {
        if (dead) return;

        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        animPlayer.SetFloat("Speed", Mathf.Abs(xMove));

        if (xMove > 0)
            transform.eulerAngles = new Vector2(0, 0);
        else if (xMove < 0)
            transform.eulerAngles = new Vector2(0, 180);
    }

    void JumpPlayer()
    {
        if (dead) return;

        if (isJump)
        {
            rbPlayer.linearVelocity = Vector2.up * jumpForce;
            isJump = false;
        }
    }

    //  MORTE 

    public void Death()
    {
        if (dead) return;
        StartCoroutine(DeathCoroutine());
        Debug.Log("MORREU");
    }

    IEnumerator DeathCoroutine()
    {
        dead = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float tempo = 1f;
        float timer = 0f;

        while (timer < tempo)
        {
            sr.enabled = !sr.enabled;
            timer += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        sr.enabled = true;

        // Desativa física
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        
        GameManager.instance.PerderVida();

        Destroy(gameObject);

        yield break;
    }


}
