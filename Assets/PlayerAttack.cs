using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class PlayerAttack : MonoBehaviour
    {

        private InputHandler inputHandler;
        public CapsuleCollider punchCollider;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
        }

        private void OnTriggerStay(Collider other)
        {
            Debug.Log("Inside Trigger");
            if (other.gameObject.tag == "Hostile" && inputHandler.punchFlag)
            {
                //other.GetComponentInParent<HostileBehavior>().Kill();
                other.gameObject.GetComponent<HostileBehavior>().Invoke("Kill", .5f);
            }
        }
    }
}
