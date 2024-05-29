using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BT
{
    public class HostileBehavior : MonoBehaviour
    {
        public float maxHealth = 30;
        private float currentHealth = 30;
        private Slider[] healthBars;
        public GameObject[] enemyRooms;


        public void Start()
        {
            currentHealth = maxHealth;
            healthBars = GetComponentsInChildren<Slider>(true);
            foreach (Slider healthBar in healthBars)
            {
                healthBar.maxValue = maxHealth;
            }

            enemyRooms = GameObject.FindGameObjectsWithTag("enemyRoom");
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
            Debug.Log("Damaged " + gameObject.name);
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Invoke("Kill", .25f);
            }
        }
    }
}
