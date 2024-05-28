using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace BT
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager instance;
        public GameObject player;

        InputHandler inputHandler;
        Animator anim;
        public float PunchTime = 0f;
        public float maxHealth = 100f;
        public float invulnTime = 0.25f;
        private float damageTimer;
        public float currentHealth;

        void Start()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            currentHealth = maxHealth;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
            inputHandler.isInteracting = anim.GetBool("isInteracting");
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
            if (inputHandler.punchFlag)
            {
                if (PunchTime >= .5f)
                {
                    inputHandler.punchFlag = false;
                    PunchTime = 0f;
                }
                else
                {
                    PunchTime += Time.deltaTime;
                }
            }
            if (damageTimer >= 0)
            {
                damageTimer -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage)
        {
            if (damageTimer <= 0)
            {
                currentHealth -= damage;
                damageTimer = invulnTime;
            }
            if (currentHealth <= 0)
            {
                currentHealth = maxHealth;
            }
        }
    }
}
