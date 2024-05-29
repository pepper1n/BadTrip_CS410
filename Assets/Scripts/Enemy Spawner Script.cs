using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;

    public Transform enemySpawnPoint;
    public bool boss;
    public Vector3 bossScale = new Vector3(5, 5, 5);
    private bool isTrippy;
    void Start()
    {
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];

        GameObject enemy = Instantiate(enemyPrefab, transform.position+new Vector3(Random.Range(1,5),0,Random.Range(1,5)), Quaternion.identity);

        if (boss) 
        {
            enemy.transform.localScale = bossScale;
        }

        enemy.SetActive(true);
                    
        isTrippy = GameObject.Find("Player").GetComponent<StateFlipping>().isTrippy;
        if (isTrippy)
        {
            // trippy on
            enemy.transform.GetChild(0).gameObject.SetActive(true);
            // dark off
            enemy.transform.GetChild(1).gameObject.SetActive(false);
        }
        else if (!isTrippy)
        {
            // dark on
            enemy.transform.GetChild(1).gameObject.SetActive(true);
            // trippy off
            enemy.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
