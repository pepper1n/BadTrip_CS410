using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

public class EnemySpawnerScript : MonoBehaviour
{
    public GameObject[] enemies;

    public Transform enemySpawnPoint;
    private HostileBehavior hostileScript;
    public bool boss;
    public Vector3 bossScale = new Vector3(5, 5, 5);
    public float bossHealthScale = 2;
    private bool isTrippy;
    void Start()
    {
        GameObject enemyPrefab = enemies[Random.Range(0, enemies.Length)];
        GameObject enemy = Instantiate(enemyPrefab, transform.position+new Vector3(Random.Range(1,5),0,Random.Range(1,5)), Quaternion.identity);
        //hostileScript[] = enemy.GetComponentsInChildren<HostileBehavior>();
        hostileScript = enemy.GetComponent<HostileBehavior>();
        if (boss) 
        {
            enemy.transform.localScale = bossScale;
            hostileScript.maxHealth *= bossHealthScale;
            hostileScript.currentHealth = hostileScript.maxHealth;
            foreach (Transform child in transform)
            {
                var blackWidowAttack = child.GetComponent<BlackWidowAttack>();
                if (blackWidowAttack != null)
                {
                    blackWidowAttack.range *= 2f;
                }

                var chaserAttack = child.GetComponent<ChaserAttack>();
                if (chaserAttack != null)
                {
                    chaserAttack.range *= 2f;
                }

                var shooterAttack = child.GetComponent<ShooterAttack>();
                if (shooterAttack != null)
                {
                    shooterAttack.range *= 1.2f;
                }

                var throwerAttack = child.GetComponent<ThrowerAttack>();
                if (throwerAttack != null)
                {
                    throwerAttack.range *= 1.2f;
                }
            }
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
