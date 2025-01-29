using UnityEngine;

public class CoinController : MonoBehaviour
{
    public GameObject Player;
    public LogicScript Logic;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        Logic = GameObject.FindGameObjectWithTag("LogicManager").GetComponent<LogicScript>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            string coinTag = gameObject.tag;
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                if(!string.IsNullOrEmpty(coinTag) && coinTag.Equals("CoinBag")) {
                    Logic.IncreaseXP(true);
                }
                else
                {
                    Logic.IncreaseXP();
                }
                    Destroy(gameObject);
            }
        }
    }
}
