using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject Player;
    public LogicScript Logic;
    private Rigidbody2D rb;
    private Animator animator;

    // Enemy attributes
    private float moveSpeed = 2.2f;

    // Flip logic
    private bool isFacingRight = true;

    void Start()
    {
        // Initialize references
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ensure the game is active and not paused
        if (Logic.IsGameActive() && !Logic.PausedGame())
        {
            MoveTowardsPlayer();
            FlipSprite();
            animator.Play("Walk");
        }
    }

    private void MoveTowardsPlayer()
    {
        rb.linearVelocity = (Player.transform.position - transform.position).normalized * moveSpeed;
    }

    private void FlipSprite()
    {
        Vector2 direction = Player.transform.position - transform.position;

        if (direction.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (direction.x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invert the x-axis
        transform.localScale = localScale;
    }
}
