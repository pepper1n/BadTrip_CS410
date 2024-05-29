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
        public float shopHealth = 0f;

        private bool DOT = false;
        private float DOTduration = 0;
        private float DOTamount = 0;
        private float DOTremaining = 0;
        private float timer = 0;

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
            if (DOT)
            {
                timer += Time.deltaTime;
                currentHealth -= DOTamount*(Time.deltaTime/DOTduration);
                DOTremaining -= DOTamount*(Time.deltaTime/DOTduration);
                if (timer >= DOTduration)
                {
                    DOT = false;
                }
            }
            if (currentHealth <= 0)
            {
                currentHealth = maxHealth + shopHealth;
            }
        }

        public void TakeDamage(float damage)
        {
            if (damageTimer <= 0)
            {
                currentHealth -= damage;
                damageTimer = invulnTime;
            }
        }

        public void InflictPoison(float damage, float duration)
        {
            if (DOT)
            {
                return;
            }
            else
            {
                UnityEngine.Debug.Log("poisoned!");
                DOT = true;
                DOTduration = duration;
                DOTamount = damage;
                DOTremaining = damage;
                timer = 0;
            }
        }
    }
}
