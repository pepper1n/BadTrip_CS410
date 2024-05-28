using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;

    public Transform enemySpawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

        GameObject enemy = Instantiate(enemyPrefab, transform.position+new Vector3(Random.Range(1,5),0,Random.Range(1,5)), Quaternion.identity);
        enemy.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
