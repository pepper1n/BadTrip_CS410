using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class HostileBehavior : MonoBehaviour
    {

        public float health = 30;

        public void Kill()
        {
            Destroy(this.gameObject);
        }

        public void TakeDamage(float damage)
        {
            Debug.Log("Damaged");
            health -= damage;
            if (health <= 0)
            {
                Invoke("Kill", .25f);
            }
        }
    }
}
