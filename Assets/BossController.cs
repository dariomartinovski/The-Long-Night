using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public GameObject Player;
    public LogicScript Logic;
    public TimerController Timer;

    private Rigidbody2D rb;
    private Animator animator; 
    // Enemy attributes
    private float MoveSpeed = 2.2f;

    // Flip logic
    private bool isFacingRight = true;
    private bool SpawnTimePassed = false;


    void Start()
    {
        // Initialize references
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        Player = GameObject.FindWithTag("Player");
        Timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerController>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Ensure the game is active and not paused
        if (Logic.IsGameActive() && !Logic.PausedGame() && HasSpawnTimePassed())
        {
            MoveTowardsPlayer();
            FlipSprite();
            animator.Play("Walk");
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            animator.Play("Idle");
        }
    }

    public bool HasSpawnTimePassed()
    {
        if (!SpawnTimePassed)
        {
            SpawnTimePassed = Timer.GetCurrentTimeInSeconds() < 585;
        }
        return SpawnTimePassed;
    }

    private void MoveTowardsPlayer()
    {
        rb.linearVelocity = (Player.transform.position - transform.position).normalized * MoveSpeed;
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
        localScale.x *= -1;
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
                playerController.TakeDamage(2);
            }
        }
    }
}
