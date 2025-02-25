using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float BulletSpeed = 10f;
    private float LifeTime = 1.5f;
    private bool SpawnTimePassed = false;

    // Components
    public LogicScript Logic;
    private CoinSpawner CoinSpawner;
    public TimerController Timer;
    public AudioManager AudioManager;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, LifeTime);
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        CoinSpawner = GameObject.FindGameObjectWithTag("CoinSpawner").GetComponent<CoinSpawner>();
        Timer = GameObject.FindGameObjectWithTag("Timer").GetComponent<TimerController>();
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void FixedUpdate() {
        rb.linearVelocity = -transform.right * BulletSpeed;
        //add a trigger later triggerEnter2d
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Enemy"))
        {
            AudioManager.PlaySFX(AudioManager.enemyHit);
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
       else if (Other.CompareTag("Boss") && HasSpawnTimePassed())
        {
            AudioManager.PlaySFX(AudioManager.bossHit);
            Boolean isBossKilled = Logic.IncreaseBossHitCount();
            if (isBossKilled)
            {
                Vector2 enemyPosition = Other.transform.position;
                // Spawn coin at the enemy's position
                CoinSpawner.BossSpawnCoin(enemyPosition);
                //Enemy
                Destroy(Other.gameObject);
                //Bullet
                Destroy(gameObject);
                Logic.Hit(true);
            }
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
}
