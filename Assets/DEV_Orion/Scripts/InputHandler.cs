using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BT
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool b_Input;
        public bool p_Input;
        public bool j_Input;

        public bool punchFlag;
        public bool rollFlag;
        public bool sprintFlag;
        public bool pickupFlag;
        public bool jumpFlag;
        public bool targetFlag;
        public float rollInputTimer;
        public bool isInteracting;

        PlayerControls inputActions;
        CameraHandler cameraHandler;
        WeaponEquip weaponEquip;

        Vector2 movementInput;
        Vector2 cameraInput;
        
        private AudioSource audioSource;
        public AudioClip[] walkSounds;
        public AudioClip[] runSounds;
        public AudioClip[] jumpSounds;
        public AudioClip[] attackSounds; 

        void Start()
        {
            cameraHandler = CameraHandler.singleton;
            weaponEquip = GetComponent<WeaponEquip>();
            audioSource = GetComponent<AudioSource>();
        }

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null)
            {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, mouseX, mouseY);
            }
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.Move.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.Move.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();

        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            HandleTargetMode();
            MoveInput(delta);
            HandleDashInput(delta);
            HandleJump();
            HandleAttackInput(delta);
            HandleWeaponEquip();
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;


            if (horizontal != 0 || vertical != 0 && !sprintFlag && GetComponent<PlayerLocomotion>().CheckIfGrounded()) 
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = walkSounds[Random.Range(0, walkSounds.Length)];
                    audioSource.Play();
                }
            }
        }

        private void HandleDashInput(float delta)
        {
            b_Input = inputActions.PlayerActions.Dash.IsPressed();
            if (b_Input)
            {
                rollInputTimer += delta;
                sprintFlag = true;

                
                if  (!audioSource.isPlaying && GetComponent<PlayerLocomotion>().CheckIfGrounded())
                {
                    audioSource.clip = runSounds[Random.Range(0, runSounds.Length)];
                    audioSource.Play();
                }
            }
            else
            {
                if (rollInputTimer > 0 && rollInputTimer < 0.5f)
                {
                    sprintFlag = false;
                    rollFlag = true;
                }

                rollInputTimer = 0;
            }
        }

        private void HandleAttackInput(float delta)
        {
            p_Input = inputActions.PlayerActions.Punch.IsPressed();
            if (p_Input)
            {
                punchFlag = true;

                if  (!audioSource.isPlaying)
                {
                    audioSource.clip = attackSounds[Random.Range(0, attackSounds.Length)];
                    audioSource.Play();
                }
            }
        }

        private void HandleWeaponEquip()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                pickupFlag = true;
            }
            else
            {
                pickupFlag = false;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                weaponEquip.SwitchWeapon(weaponEquip.Weapon1);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                weaponEquip.SwitchWeapon(weaponEquip.Weapon2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                weaponEquip.SwitchWeapon(weaponEquip.Weapon3);
            }
        }

        private void HandleJump()
        {
            j_Input = inputActions.PlayerActions.Jump.IsPressed();
            if (j_Input)
            {
                jumpFlag = true;
                if  (!audioSource.isPlaying)
                {
                    audioSource.clip = jumpSounds[Random.Range(0, jumpSounds.Length)];
                    audioSource.Play();
                }
            }
            else
            {
                jumpFlag = false;
            }

        }

        private void HandleTargetMode()
        {
            if (Input.GetMouseButton(1))
            {
                targetFlag = true;
            }
            else
            {
                targetFlag = false;
            }
        }
    }
}
