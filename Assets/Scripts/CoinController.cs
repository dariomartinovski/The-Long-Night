using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject Player;
    public LogicScript Logic;
    public AudioManager AudioManager;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
        AudioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string coinTag = gameObject.tag;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if(!string.IsNullOrEmpty(coinTag) && coinTag.Equals("CoinBag"))
                {
                    AudioManager.PlaySFX(AudioManager.coinBagPickup);
                    Logic.IncreaseXP(true);
                }
                else
                {
                    AudioManager.PlaySFX(AudioManager.coinPickup);
                    Logic.IncreaseXP();
                }
                Destroy(gameObject);
            }
        }
    }
}
