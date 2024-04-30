using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class PlayerManager : MonoBehaviour
    {

        InputHandler inputHandler;
        Animator anim;
        public float PunchTime = 0f;

        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponent<Animator>();
        }

        void Update()
        {
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
            
        }
    }
}
