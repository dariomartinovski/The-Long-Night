using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;


public class LogicScript : MonoBehaviour
{
    public int CurretnLives = 5;
    public int Score = 0;
    public int XP = 0;
    public int Level = 1;
    public int CurrentNeededXPForLevel = 20;
    private const int XP_RATE_INCREASE = 2;
    
    private int BossHitCount = 0;
    private const int MaxBossHits = 50;

    private bool GamePaused = false;
    private bool GameActive = false;

    public Text ScoreDisplay;
    public Text TotalScore;
    public Text EnemiesKilled;
    public Text HighScore;
    public Text StartScreenHighScore;
    public Text LevelDisplay;
    public Text CurrentXPLabel;
    public Text CurrentBossHealthLabel;
    public Text LevelFinal;
    
    public GameObject GameFinishedScreen;
    public GameObject gameWonScreen;
    public GameObject gameLostScreen;
    public GameObject gamePausedScreen;
    public GameObject startGameScreen;
    public Slider slider;
    public Slider healthBarSlider;

    private bool SomethingDispalyed = false;

    public void Start() {
        PlayerPrefs.SetInt("HighScore", Math.Max(PlayerPrefs.GetInt("HighScore"), 0));
        SetKillsCounter();
        StartScreenHighScore.text = "High score: " + PlayerPrefs.GetInt("HighScore").ToString();
        SetMaxXP(CurrentNeededXPForLevel);
        SetLevel();
        SetCurrentXPLabel();
        healthBarSlider.maxValue = MaxBossHits;
        healthBarSlider.value = MaxBossHits;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameActive)
        {
            //pause game
            if (GamePaused)
            {
                gamePausedScreen.SetActive(false);
            }
            else
            {
                gamePausedScreen.SetActive(true);
            }
            GamePaused = !GamePaused;
        }
    }

    public void TakeDamage(int num = 1) {
        CurretnLives -= num;
        ScoreDisplay.text = CurretnLives.ToString();
    }

    public void Hit(Boolean isBoss=false) {
        Score += isBoss ? 50 : 1;
        SetKillsCounter();
    }

    public Boolean IncreaseBossHitCount()
    {
        BossHitCount++;
        SetCurrentBossHealth();
        if (BossHitCount >= MaxBossHits)
        {
            BossHitCount = 0;
            return true;
        }
        return false;
    }

    public void SetKillsCounter() {
        ScoreDisplay.text = "Score: " + Score.ToString();
    }

    public void SetLevel()
    {
        LevelDisplay.text = "Level: " + Level.ToString();
    }

    public void SetCurrentXPLabel()
    {
        CurrentXPLabel.text = String.Format("{0}/{1}", XP, CurrentNeededXPForLevel);
    }

    public void SetCurrentBossHealth()
    {
        int CurrentBossHealth = MaxBossHits - BossHitCount;
        healthBarSlider.value = CurrentBossHealth;
        CurrentBossHealthLabel.text = String.Format("{0}/{1}", CurrentBossHealth, MaxBossHits);
    }

    public void GameOver()
    {
        if (!SomethingDispalyed)
        {
            gameLostScreen.SetActive(true);
            DisplayInfo();
            SomethingDispalyed = true;
        }
    }

    public void GameWon()
    {
        if (!SomethingDispalyed)
        {
            gameWonScreen.SetActive(true);
            DisplayInfo();
            SomethingDispalyed = true;
        }
    }

    public bool PausedGame() {
        //make game paused screen
        return GamePaused;
    }

    public void ResumeGame() {
        if (GamePaused)
        {
            gamePausedScreen.SetActive(false);
        }
        else
        {
            gamePausedScreen.SetActive(true);
        }
        GamePaused = !GamePaused;
    }

    public void SetMaxXP(int xp)
    {
        slider.maxValue = xp;
        slider.value = 0;
    }

    public void IncreaseXP(Boolean isBoss=false)
    {
        XP += isBoss ? 10 : 1;
        slider.value = XP;
        if(XP >= slider.maxValue)
        {
            XP = 0;
            slider.value = 0;
            Level++;
            SetLevel();
            CurrentNeededXPForLevel += XP_RATE_INCREASE;
            SetMaxXP(CurrentNeededXPForLevel);
        }
        SetCurrentXPLabel();
    }

    public void DisplayInfo()
    {
        GameActive = false;
        GameFinishedScreen.SetActive(true);

        // Set high score
        PlayerPrefs.SetInt("HighScore", Math.Max(PlayerPrefs.GetInt("HighScore"), Score * 10));
        
        TotalScore.text = "Score: " + (Score * 10).ToString();
        EnemiesKilled.text = "Total enemies killed: " + Score.ToString();
        LevelFinal.text = "Level: " + Level.ToString();
        HighScore.text = "High score: " + PlayerPrefs.GetInt("HighScore").ToString();

        /*PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.StopMovement();

        EnemySpawner enemySpawner = FindObjectsOfType<EnemySpawner>()[0];
        enemySpawner.StopSpawning();

        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in enemies)
        {
            enemy.StopMovement();
        }*/
    }

    public bool IsGameActive() {
        return GameActive;
    }

    public void RestartGame() {
        GameActive = false;
        startGameScreen.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame() {
        startGameScreen.SetActive(false);
        GameActive = true;
    }

    public void ExitGame() {
        Application.Quit();
    }
}
