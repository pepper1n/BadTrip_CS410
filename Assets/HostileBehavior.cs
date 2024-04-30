using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class HostileBehavior : MonoBehaviour
    {
        public void Kill()
        {
            Destroy(this.gameObject);
        }
    }
}
