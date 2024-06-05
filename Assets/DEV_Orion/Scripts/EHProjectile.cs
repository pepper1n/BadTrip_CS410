using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class EHProjectile : MonoBehaviour
    {
        Rigidbody rb;
        CapsuleCollider damager;
        public float damage = 10f;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            damager = GetComponent<CapsuleCollider>();
        }
        public void Travel()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 50f, ForceMode.Impulse);
        }

        void OnTriggerEnter(Collider other)
        {
            HostileBehavior otherHostile = other.gameObject.GetComponent<HostileBehavior>();
            HostileBehavior parentHostile = other.gameObject.GetComponentInParent<HostileBehavior>();

            if (otherHostile != null)
            {
                otherHostile.TakeDamage(damage);
                Destroy(gameObject);
            }
            else if (parentHostile != null)
            {
                parentHostile.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
