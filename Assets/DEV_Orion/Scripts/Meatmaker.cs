using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class Meatmaker : MonoBehaviour
    {
        public GameObject player;
        private InputHandler inputHandler;
        public CapsuleCollider attackCollider;
        private StateFlipping stateScript;
        public Animator anim;

        public float damage = 2f;
        public float attackDelay = .1f;
        public float attackTimer = .1f;

        //private Shop shopScript;

        //private AudioSource audioSource;
        //public AudioClip chainsawSounds;
        // Start is called before the first frame update
        void Start()
        {
            inputHandler = player.GetComponent<InputHandler>();
            attackCollider = GetComponent<CapsuleCollider>();
            stateScript = player.GetComponent<StateFlipping>();
            //audioSource = GetComponent<AudioSource>();
            //shopScript = FindObjectOfType<Shop>();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (attackTimer < attackDelay)
            {
                attackTimer += Time.deltaTime;
            }
            HandleSpin();
        }

        private void OnTriggerStay(Collider other)
        {
            if (inputHandler.punchFlag && attackTimer >= attackDelay)
            {
                HostileBehavior otherHostile = other.gameObject.GetComponent<HostileBehavior>();
                HostileBehavior parentHostile = other.gameObject.GetComponentInParent<HostileBehavior>();

                if (otherHostile != null)
                {
                    otherHostile.TakeDamage(damage);
                    Debug.Log($"damage done: {damage}");
                    stateScript.timer += damage;
                    attackTimer = 0f;
                }
                else if (parentHostile != null)
                {
                    parentHostile.TakeDamage(damage);
                    stateScript.timer += damage;
                    attackTimer = 0f;
                }
            }

            if (inputHandler.punchFlag && attackTimer >= attackDelay && other.gameObject.tag == "merchant")
            {
                //shopScript.dead();
            }
        }
        private void HandleSpin()
        {
            if (inputHandler.punchFlag)
            {
                anim.SetBool("IsSpinning", true);
            }
            else
            {
                anim.SetBool("IsSpinning", false);
            }
        }
    }
}
