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

        public void Start()
        {
            currentHealth = maxHealth;
            healthBars = GetComponentsInChildren<Slider>(true);
            foreach (Slider healthBar in healthBars)
            {
                healthBar.maxValue = maxHealth;
            }
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
            Destroy(this.gameObject);
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Damaged");
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                Invoke("Kill", .25f);
            }
        }
    }
}
