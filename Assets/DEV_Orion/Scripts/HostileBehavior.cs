using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BT
{
    public class HostileBehavior : MonoBehaviour
    {
        public GameObject coin;
        private PlayerLocomotion pl;
        public float maxHealth = 30;
        public float currentHealth = 30;
        private Slider[] healthBars;
        public GameObject[] enemyRooms;
        private UnityEngine.AI.NavMeshAgent[] agents;
        private Coroutine rageRemovalCoroutine;
        public bool isRaged;
        private StateFlipping stateScript;
        private bool isTrippy;
        private Transform coinLocation;
        public GameObject player;
        public Vector3 spawnRotation;
        Quaternion rotation;


        public void Start()
        {
            spawnRotation = new Vector3(-90, 0, 0);
            rotation = Quaternion.Euler(spawnRotation);
            player = GameObject.FindWithTag("Player");
            pl = FindObjectOfType<PlayerLocomotion>();
            currentHealth = maxHealth;
            healthBars = GetComponentsInChildren<Slider>(true);
            foreach (Slider healthBar in healthBars)
            {
                healthBar.maxValue = maxHealth;
            }
            stateScript = FindObjectOfType<StateFlipping>();
            enemyRooms = GameObject.FindGameObjectsWithTag("enemyRoom");
            agents = GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>(true);
        }

        public void Update()
        {
            isTrippy = stateScript.isTrippy;
            foreach (Slider healthBar in healthBars)
            {
                if (currentHealth == maxHealth || currentHealth <= 0)
                {
                    healthBar.gameObject.SetActive(false);
                }
                else
                {
                    healthBar.gameObject.SetActive(true);
                }
                
                healthBar.value = currentHealth;
            }
        }

        public void Kill()
        {
            Debug.Log("Killed " + gameObject.name);
            Transform[] children = GetComponentsInChildren<Transform>(true);
            foreach (Transform child in children)
            {
                if (child.CompareTag("trippyStructure") || child.CompareTag("fleshStructure"))
                {
                    if (child.gameObject.activeSelf)
                    {
                        coinLocation = child.transform;
                        RaycastHit hit;
                        if (Physics.Raycast(coinLocation.position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
                        {
                            //Vector3 coinPosition = hit.point;
                            //GameObject enemy = Instantiate(coin, coinPosition, Quaternion.identity);
                            //Debug.Log("Coin dropped");
                        }
                    }
                }
            }
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(gameObject);
            GameObject drop = Instantiate(coin, player.transform.position + player.transform.forward *2 + new Vector3(0, 1, 0), rotation);
            drop.SetActive(true);
            Destroy(this.gameObject);

            
            
            foreach (GameObject room in enemyRooms)
            {
                room.GetComponent<ActiveRoom>().enemyKill(gameObject);
            }
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Damaged");
            currentHealth -= (damage + pl.shopDamage);
            if (currentHealth <= 0)
            {
                Invoke("Kill", .25f);
            }
        }

        public void ApplyRage(float intensity)
        {
            foreach (var agent in agents)
            {
                if (!isRaged)
                {
                    agent.speed *= intensity;
                    isRaged = true;
                    foreach (Transform child in transform)
                    {
                        var blackWidowAttack = child.GetComponent<BlackWidowAttack>();
                        if (blackWidowAttack != null)
                        {
                            blackWidowAttack.attackDelay /= intensity;
                        }

                        var chaserAttack = child.GetComponent<ChaserAttack>();
                        if (chaserAttack != null)
                        {
                            chaserAttack.attackDelay /= intensity;
                        }

                        var shooterAttack = child.GetComponent<ShooterAttack>();
                        if (shooterAttack != null)
                        {
                            shooterAttack.attackDelay /= intensity;
                        }

                        var throwerAttack = child.GetComponent<ThrowerAttack>();
                        if (throwerAttack != null)
                        {
                            throwerAttack.attackDelay /= intensity;
                        }
                    }
                }
            }
        }

        public void RemoveRage(float intensity)
        {
            foreach (var agent in agents)
            {
                if (isRaged)
                {
                    agent.speed /= intensity;
                    isRaged = false;
                    foreach (Transform child in transform)
                    {
                        var blackWidowAttack = child.GetComponent<BlackWidowAttack>();
                        if (blackWidowAttack != null)
                        {
                            blackWidowAttack.attackDelay *= intensity;
                        }

                        var chaserAttack = child.GetComponent<ChaserAttack>();
                        if (chaserAttack != null)
                        {
                            chaserAttack.attackDelay *= intensity;
                        }

                        var shooterAttack = child.GetComponent<ShooterAttack>();
                        if (shooterAttack != null)
                        {
                            shooterAttack.attackDelay *= intensity;
                        }

                        var throwerAttack = child.GetComponent<ThrowerAttack>();
                        if (throwerAttack != null)
                        {
                            throwerAttack.attackDelay *= intensity;
                        }
                    }
                }
            }
        }

        public void DelayRageRemoval(float duration, float intensity)
        {
            if (rageRemovalCoroutine != null)
            {
                StopCoroutine(rageRemovalCoroutine);
            }
            rageRemovalCoroutine = StartCoroutine(DelayRageRemovalCoroutine(duration, intensity));
        }

        public void CancelRageRemoval()
        {
            if (rageRemovalCoroutine != null)
            {
                StopCoroutine(rageRemovalCoroutine);
                rageRemovalCoroutine = null;
            }
        }

        private IEnumerator DelayRageRemovalCoroutine(float duration, float intensity)
        {
            yield return new WaitForSeconds(duration);
            RemoveRage(intensity);
        }
    }
    
}
