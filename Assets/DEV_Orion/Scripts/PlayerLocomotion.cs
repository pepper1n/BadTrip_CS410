using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    public class PlayerLocomotion : MonoBehaviour
    {
        public float shopSpeed = 0;
        public float shopDamage = 0;
        public float shopJump = 0;
        public float shopHealth = 0;
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;
        private Coroutine slowRemovalCoroutine;

        [HideInInspector]
        public Transform myTransform;

        public Transform RootTransform;

        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        public bool isSprinting;

        [Header("Stats")]
        [SerializeField]
        public float gold = 999;
        [SerializeField]
        public float movementSpeed = 5;
        [SerializeField]
        public float sprintSpeed = 7;
        [SerializeField]
        float rotationSpeed = 10;
        [SerializeField]
        public float jumpForce = 1000;
        public float jumpDelay = 0.2f;
        float jumpTimer = 0;
        public float jumpCount = 1;
        float jumps = 0;
        public bool isSlowed = false;
        //public bool falling = false;

        void Start()
        {
            if (cameraObject == null)
            {
                cameraObject = CameraHandler.singleton.transform;
            }
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponent<AnimatorHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        private void HandleRotation(float delta)
        {
            if (inputHandler.targetFlag)
            {
                myTransform.rotation = Quaternion.LookRotation(cameraObject.forward);
            }
            else
            {
                Vector3 targetDir = Vector3.zero;
                float moveOverride = inputHandler.moveAmount;

                targetDir = cameraObject.forward * inputHandler.vertical;
                targetDir += cameraObject.right * inputHandler.horizontal;

                targetDir.Normalize();
                targetDir.y = 0;

                if (targetDir == Vector3.zero)
                {
                    targetDir = myTransform.forward;
                }

                float rs = rotationSpeed;
                Quaternion tr = Quaternion.LookRotation(targetDir);
                Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

                myTransform.rotation = targetRotation;
            }
        }


        public void Update()
        {
            float delta = Time.deltaTime;
            isSprinting = inputHandler.b_Input;
            inputHandler.TickInput(delta);
            if (gameObject != null)
            {
                HandleJump(delta);
                HandleMovement(delta);
                HandleRollingAndSprinting(delta);
                HandleAttack(delta);
            }
        }

        public void HandleMovement(float delta)
        {
            if (inputHandler.rollFlag)
            {
                return;
            }
            if (transform != null)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;
                moveDirection.Normalize();
            }

            float speed = movementSpeed + shopSpeed;
            if (inputHandler.sprintFlag)
            {
                speed = sprintSpeed + shopSpeed;
                isSprinting = true;
                moveDirection *= speed;

            }
            else
            {
                moveDirection *= speed;
            }

            //if (!animatorHandler.anim.GetBool("isInteracting"))
            //{
            //    moveDirection.y = 0;
            //}

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = new Vector3(projectedVelocity.x, rigidbody.velocity.y, projectedVelocity.z);

            animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, isSprinting);

            if (animatorHandler.canRotate)
            {
                HandleRotation(delta);
            }
        }

        public void HandleRollingAndSprinting(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
            {
                return;
            }
            if (inputHandler.rollFlag)
            {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0)
                {
                    animatorHandler.PlayTargetAnimation("Dashing", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;

                }
                else
                {
                    animatorHandler.PlayTargetAnimation("Dash Back", true);
                }
            }
        }
        #endregion

        public void HandleAttack(float delta)
        {
            if (animatorHandler.anim.GetBool("isInteracting"))
            {
                return;
            }
            if (inputHandler.punchFlag && !(inputHandler.rollFlag))
            {
                animatorHandler.PlayTargetAnimation("Punch", true);
            }
        }

        public bool CheckIfGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, .5f))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void HandleJump(float delta)
        {
            if (CheckIfGrounded())
            {
                jumps = 0;
                jumpTimer = jumpDelay;
                //falling = false;
            }
            else
            {
                //falling = true;
            }
            if (animatorHandler.anim.GetBool("isInteracting"))
            {
                return;
            }
            if (inputHandler.jumpFlag && jumpTimer >= jumpDelay && jumps < jumpCount)
            {
                jumps += 1;
                jumpTimer = 0;
                animatorHandler.PlayTargetAnimation("Jump", false);
                rigidbody.AddForce(new Vector3(0, 1, 0) * (jumpForce + shopJump * 10) * delta, ForceMode.Impulse);
            }
            else
            {
                jumpTimer += delta;
            }
        }

        public void ApplySlow(float intensity)
        {
            if (!isSlowed)
            {
                movementSpeed *= intensity;
                sprintSpeed *= intensity;
                isSlowed = true;
            }
        }

        public void RemoveSlow(float intensity)
        {
            if (isSlowed)
            {
                movementSpeed /= intensity;
                sprintSpeed /= intensity;
                isSlowed = false;
            }
        }

        public void DelaySlowRemoval(float duration, float intensity)
        {
            if (slowRemovalCoroutine != null)
            {
                StopCoroutine(slowRemovalCoroutine);
            }
            slowRemovalCoroutine = StartCoroutine(DelaySlowRemovalCoroutine(duration, intensity));
        }

        public void CancelSlowRemoval()
        {
            if (slowRemovalCoroutine != null)
            {
                StopCoroutine(slowRemovalCoroutine);
                slowRemovalCoroutine = null;
            }
        }

        private IEnumerator DelaySlowRemovalCoroutine(float duration, float intensity)
        {
            yield return new WaitForSeconds(duration);
            RemoveSlow(intensity);
        }
    }
}
