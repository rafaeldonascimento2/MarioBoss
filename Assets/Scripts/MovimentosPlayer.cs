using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rbPlayer;
    [SerializeField] private float speed = 5f;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed, rbPlayer.linearVelocity.y);

        if (xMove > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (xMove < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}
