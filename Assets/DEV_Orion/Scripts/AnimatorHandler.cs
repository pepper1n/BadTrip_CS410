using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class AnimatorHandler : MonoBehaviour
    {
        public Animator anim;
        public InputHandler inputHandler;
        public PlayerLocomotion playerLocomotion;
        public WeaponEquip WE;
        public StateFlipping SF;
        int vertical;
        int horizontal;
        public bool canRotate;
        public bool staffAnim = false;
        public bool meatAnim = false;

        public void Initialize()
        {
            anim = GetComponent<Animator>();
            inputHandler = GetComponent<InputHandler>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
            WE = GetComponent<WeaponEquip>();
            SF = GetComponent<StateFlipping>();
        }

        public void Update()
        {
            WeaponAnimHandler();
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting)
        {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                v = 0.5f;
            }
            else if (verticalMovement >= .55f)
            {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                v = -0.5f;
            }
            else if (verticalMovement <= -0.55f)
            {
                v = -1;
            }
            else
            {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                h = 0.5f;
            }
            else if (horizontalMovement >= .55f)
            {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                h = -0.5f;
            }
            else if (horizontalMovement <= -0.55f)
            {
                h = -1;
            }
            else
            {
                h = 0;
            }
            #endregion

            if (isSprinting && v != 0)
            {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);

        }

        public void PlayTargetAnimation(string targetAnim, bool isInteracting)
        {
            anim.applyRootMotion = isInteracting;
            anim.SetBool("isInteracting", isInteracting);
            anim.CrossFade(targetAnim, .2f);
        }

        public void CanRotate()
        {
            canRotate = true;
        }

        public void StopRotation()
        {
            canRotate = false;
        }

        private void OnAnimatorMove()
        {
            if (inputHandler.isInteracting == false)
            {
                return;
            }
            float delta = Time.deltaTime;
            playerLocomotion.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerLocomotion.rigidbody.velocity = velocity;

        }

        public void WeaponAnimHandler()
        {
            if (WE.activeWeapon == WE.EVMM)
            {
                if (SF.isTrippy)
                {
                    anim.SetBool("MMHold", false);
                    anim.SetBool("StaffHold", true);
                }
                else
                {
                    //meatmaker animation logic
                    anim.SetBool("StaffHold", false);
                    anim.SetBool("MMHold", true);

                }
            }
            else
            {
                anim.SetBool("StaffHold", false);
                anim.SetBool("MMHold", false);
            }
        }

    }
}
