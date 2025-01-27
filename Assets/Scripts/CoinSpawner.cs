using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public List<GameObject> Coins;

    public void SpawnCoin(Vector2 position)
    {
        if (Coins != null && Coins.Count > 0)
        {
            int randomIndex = Random.Range(0, Coins.Count);
            GameObject coinToSpawn = Coins[randomIndex];

            Instantiate(coinToSpawn, position, Quaternion.identity);
            //Instantiate(coinToSpawn, position, transform.rotation);
        }
    }
}

