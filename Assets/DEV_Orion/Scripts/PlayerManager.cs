using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BT
{
    public class PlayerManager : MonoBehaviour
    {

        public static PlayerManager instance;
        public PlayerLocomotion pl;
        public GameObject player;
        public Vector3 startingPosition;

        private Rigidbody rb;
        private StateFlipping stateScript;

        InputHandler inputHandler;
        Animator anim;

        public float PunchTime = 0f;
        public float maxHealth = 100f;
        public float invulnTime = 0.25f;
        public float currentHealth;
        public float shopHealth = 0f;
        public float trippyRegen = 10f;

        private bool isTrippy;
        private bool DOT = false;
        private bool isStandingStill;
        private float DOTduration = 0;
        private float DOTamount = 0;
        private float DOTremaining = 0;
        private float timer = 0;
        private float speedTimer = 0;
        private float damageTimer;

        bool sb = false;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            pl = player.GetComponent<PlayerLocomotion>();
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
            startingPosition = transform.position;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponent<Animator>();
            rb = GetComponent<Rigidbody>();
            stateScript = GetComponent<StateFlipping>();
        }

        void Update()
        {
            if(currentHealth <= 0f)
            {
                Die();
            }
            isTrippy = stateScript.isTrippy;
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
                anim.applyRootMotion = true;
                anim.SetBool("isInteracting", true);
                anim.CrossFade("Death", .1f);
            }

            if(sb == true)
            {
                speedTimer += Time.deltaTime;
                if(speedTimer == 15)
                {
                    pl.shopSpeed -= 10;
                    sb = false;
                }
            }

            if (rb != null)
            {
                isStandingStill = rb.velocity.magnitude < 0.1f;
            }

            if (isTrippy && isStandingStill)
            {
                HealPlayer();
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

        public void Die()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("Start");

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

        public void speedBoost()
        {
            pl.shopSpeed +=10;
            sb = true;
        }

        void HealPlayer()
        {
            currentHealth += trippyRegen * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        }
    }
}
