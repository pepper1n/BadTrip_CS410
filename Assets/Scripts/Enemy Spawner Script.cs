using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
