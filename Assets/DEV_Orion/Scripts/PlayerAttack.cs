using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class PlayerAttack : MonoBehaviour
    {
        public GameObject player;
        private InputHandler inputHandler;
        public CapsuleCollider attackCollider;
        public float swingSpeed = .5f;
        public float damage = 10f;
        public float attackDelay = 1f;
        public float attackTimer = 1f;
        private AudioSource audioSource;
        public AudioClip[] hammerAttackSounds;
        public AudioClip[] swordAttackSounds;

        public PlayerLocomotion pl;
        private Shop shopScript;

        // Start is called before the first frame update
        void Start()
        {
            shopScript = FindObjectOfType<Shop>();
            inputHandler = player.GetComponent<InputHandler>();
            attackCollider = GetComponent<CapsuleCollider>();
            audioSource = GetComponent<AudioSource>();
        }

        void Update()
        {
            if (attackTimer < attackDelay)
            {
                attackTimer += Time.deltaTime;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (inputHandler.punchFlag && attackTimer >= attackDelay)
            {
                HostileBehavior otherHostile = other.gameObject.GetComponent<HostileBehavior>();
                HostileBehavior parentHostile = other.gameObject.GetComponentInParent<HostileBehavior>();

                if (otherHostile != null)
                {
                    otherHostile.TakeDamage(damage + pl.shopDamage);
                    attackTimer = 0f;
                }
                else if (parentHostile != null)
                {
                    parentHostile.TakeDamage(damage + pl.shopDamage);
                    attackTimer = 0f;
                }
            }

            if (inputHandler.punchFlag && attackTimer >= attackDelay && other.gameObject.tag == "merchant")
            {
                shopScript.dead();
            }
        }
    }
}
