using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class ResetIsInteracting : StateMachineBehaviour
    {
        //public float dodgeRotation = 90;
        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    Transform character = animator.GetComponentInParent<Transform>();
        //    //Vector3 Rotate = new Vector3(0, 90, 0);
        //    character.Rotate(0, dodgeRotation, 0);
        //}

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    
        //}

        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("isInteracting", false);
            //Transform character = animator.GetComponentInParent<Transform>();
            //character.Rotate(0, -1 * dodgeRotation, 0);
        }

        // OnStateMove is called right after Animator.OnAnimatorMove()
        //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that processes and affects root motion
        //}

        // OnStateIK is called right after Animator.OnAnimatorIK()
        //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        //{
        //    // Implement code that sets up animation IK (inverse kinematics)
        //}
    }
}
