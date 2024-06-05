using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawn : MonoBehaviour
{
    public int coinSpawnChance = 50;

    // Start is called before the first frame update
    void Start()
    {
        // Check if a coin should spawn based on the spawn chance
        if (Random.Range(0, 100) < coinSpawnChance)
        {
            gameObject.SetActive(true);
        }
        else 
        {
            gameObject.SetActive(false);
        }
    }

}
