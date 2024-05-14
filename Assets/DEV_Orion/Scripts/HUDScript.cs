using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class HUDScript : MonoBehaviour
    {

        public InputHandler IH;
        public GameObject Reticle;
        // Start is called before the first frame update
        void Start()
        {
            IH = GameObject.Find("Player").GetComponent<InputHandler>();
            Reticle.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (IH.targetFlag)
            {
                Reticle.SetActive(true);
            }
            else
            {
                Reticle.SetActive(false);
            }
        }
    }
}
