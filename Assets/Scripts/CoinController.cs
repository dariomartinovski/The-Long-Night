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
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Logic.IncreaseXP();
                Destroy(gameObject);
            }
        }
    }
}
