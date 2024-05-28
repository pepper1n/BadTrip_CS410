using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BT
{
    public class HUDScript : MonoBehaviour
    {

        public InputHandler IH;
        public GameObject Reticle;
        public Slider trippyBar;
        public Slider fleshBar;

        private StateFlipping stateScript;
        // Start is called before the first frame update
        void Start()
        {
            stateScript = GameObject.Find("Player").GetComponent<StateFlipping>();
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

            if (stateScript.isTrippy)
            {
                trippyBar.maxValue = stateScript.trippyDuration;
                trippyBar.value = stateScript.timer;
            }
            else
            {
                fleshBar.maxValue = stateScript.fleshDuration;
                fleshBar.value = stateScript.timer;
            }
        }
    }
}
