using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BT
{
    public class HostileBehavior : MonoBehaviour
    {
        private PlayerLocomotion pl;
        public float maxHealth = 30;
        private float currentHealth = 30;
        private Slider[] healthBars;
        public GameObject[] enemyRooms;
        public UnityEngine.AI.NavMeshAgent[] agents;
        private Coroutine rageRemovalCoroutine;
        public bool isRaged;

        public void Start()
        {
            pl = FindObjectOfType<PlayerLocomotion>();
            currentHealth = maxHealth;
            healthBars = GetComponentsInChildren<Slider>(true);
            foreach (Slider healthBar in healthBars)
            {
                healthBar.maxValue = maxHealth;
            }

            enemyRooms = GameObject.FindGameObjectsWithTag("enemyRoom");
            agents = GetComponentsInChildren<UnityEngine.AI.NavMeshAgent>(true);
        }

        public void Update()
        {
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
