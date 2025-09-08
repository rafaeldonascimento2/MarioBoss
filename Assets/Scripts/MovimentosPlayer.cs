using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // --- Refs ---
    private Rigidbody2D rbPlayer;

    [Header("Move")]
    [SerializeField] private float speed = 5f;

    [Header("Run (Double-Tap)")]
    [SerializeField] private float runMultiplier = 1.7f;   // velocidade extra ao correr
    [SerializeField] private float doubleTapWindow = 0.25f; // tempo máximo entre toques
    [SerializeField] private float runGrace = 0.2f;         // quanto tempo mantém corrida sem input

    private float lastLeftTapTime  = -10f;
    private float lastRightTapTime = -10f;
    private bool running = false;
    private int runDir = 0; // -1 esquerda, +1 direita
    private float runTimer = 0f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float lowJumpCut = 0.4f; // <1 corta a subida ao soltar

    // inputs
    private bool jumpPressed;
    private bool jumpHeld;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // ----- Jump input -----
        if (Input.GetButtonDown("Jump")) jumpPressed = true;
        jumpHeld = Input.GetButton("Jump");

        // ----- Double-tap detection -----
        // esquerda
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (Time.time - lastLeftTapTime <= doubleTapWindow)
            {
                running = true;
                runDir = -1;
                runTimer = runGrace;
            }
            lastLeftTapTime = Time.time;
        }
        // direita
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (Time.time - lastRightTapTime <= doubleTapWindow)
            {
                running = true;
                runDir = +1;
                runTimer = runGrace;
            }
            lastRightTapTime = Time.time;
        }

        // se não houver input contínuo na direção da corrida, deixa um "gracetime"
        float xRaw = Mathf.Sign(Input.GetAxisRaw("Horizontal"));
        if (xRaw == runDir && xRaw != 0)
            runTimer = runGrace;
        else
            runTimer -= Time.deltaTime;

        if (runTimer <= 0f) running = false;
    }

    private void FixedUpdate()
    {
        // chão
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // pulo
        if (jumpPressed && isGrounded)
        {
            var v = rbPlayer.linearVelocity; // troque por linearVelocity se seu Unity exigir
            v.y = jumpForce;
            rbPlayer.linearVelocity = v;
        }

        // cortar a subida se soltar o botão
        if (!jumpHeld && rbPlayer.linearVelocity.y > 0f)
        {
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * lowJumpCut);
        }

        // movimento
        Move();

        jumpPressed = false;
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        float currentSpeed = speed;

        // aplicar corrida se direção bate com o input atual
        if (running && Mathf.Sign(xMove) == runDir && xMove != 0f)
            currentSpeed *= runMultiplier;

        rbPlayer.linearVelocity = new Vector2(xMove * currentSpeed, rbPlayer.linearVelocity.y);

        if (xMove > 0)       transform.eulerAngles = new Vector3(0, 0, 0);
        else if (xMove < 0)  transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
