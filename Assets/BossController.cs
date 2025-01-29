using UnityEngine;
using System.Collections;

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
        rb = gameObject.GetComponent<Rigidbody2D>();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle collision with the player
            // Reduce hero's health or trigger an attack
            // You can access the hero's script or health component and call appropriate functions
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                //TODO add collision sound
                playerController.TakeDamage(2); // Example: Reduce hero's lives by 2
            }
        }
    }
}
