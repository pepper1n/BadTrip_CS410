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

        // Start is called before the first frame update
        void Start()
        {
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
            //Debug.Log("Inside Trigger");
            if (other.gameObject.tag == "Hostile" && inputHandler.punchFlag && attackTimer >= attackDelay)
            {
                //other.GetComponentInParent<HostileBehavior>().Kill();
                other.gameObject.GetComponent<HostileBehavior>().TakeDamage(damage);
                attackTimer = 0f;

            }
        }
    }
}
