using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float BulletSpeed = 10f;
    private float LifeTime = 1.5f;
    
    // Components
    public LogicScript Logic;
    private CoinSpawner CoinSpawner;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, LifeTime);
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        CoinSpawner = GameObject.FindGameObjectWithTag("CoinSpawner").GetComponent<CoinSpawner>();
    }

    private void FixedUpdate() {
        rb.linearVelocity = -transform.right * BulletSpeed;
        //add a trigger later triggerEnter2d
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            // Get the position of the enemy
            Vector2 enemyPosition = Other.transform.position;
            // Spawn coin at the enemy's position
            CoinSpawner.SpawnCoin(enemyPosition);
            //Enemy
            Destroy(Other.gameObject);
            //Bullet
            Destroy(gameObject);
            //Set Kill Count (Score)
            Logic.Hit();
        }
    }
}
