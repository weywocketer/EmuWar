using FubarOps.Combat;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using FubarOps.Constants;

namespace FubarOps.Movement
{
    /// <summary>
    /// Responsible for  calculating velocity and handling other actions based on player's input.
    /// </summary>
    [RequireComponent(typeof(PlayerInput))]
    public class ManualSteering : MonoBehaviour, ISteering
    {
        public float radius = 0.25f;
        public float Radius
        {
            get { return radius; }
        }
        Vector2 movementInput;
        public float walkSpeed = 10f;
        public float runSpeed = 15f;
        float speed;
        PlayerMode playerMode = PlayerMode.normal;

        public event Action<PlayerMode> OnPlayerModeSwitched;

        DistanceAttack distanceAttack;
        MyPlayerControls myPlayerControls;


        void Awake()
        {
            distanceAttack = GetComponent<DistanceAttack>();
            myPlayerControls = new MyPlayerControls();
            speed = walkSpeed;
        }

        void OnEnable()
        {
            myPlayerControls.Infantry.Enable();
        }

        void OnDisable()
        {
            myPlayerControls.Infantry.Disable();
        }

        public Vector3 CalculateVelocity()
        {
            return movementInput * speed;
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (playerMode != PlayerMode.normal)
            {
                return;
            }
            if (context.performed)
            {
                movementInput = context.ReadValue<Vector2>();
            }
            if (context.canceled)
            {
                movementInput = Vector2.zero;
            }
        }

        public void Binoculars(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (playerMode == PlayerMode.usingBinoculars)
                {
                    playerMode = PlayerMode.normal;
                    OnPlayerModeSwitched?.Invoke(playerMode);
                }
                else
                {
                    playerMode = PlayerMode.usingBinoculars;
                    movementInput = Vector2.zero; // Stop movement when using binoculars.
                    OnPlayerModeSwitched?.Invoke(playerMode);
                }
            }
        }

        public void Map(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (playerMode == PlayerMode.usingMap)
                {
                    playerMode = PlayerMode.normal;
                    OnPlayerModeSwitched?.Invoke(playerMode);
                }
                else
                {
                    playerMode = PlayerMode.usingMap;
                    movementInput = Vector2.zero; // Stop movement when using map.
                    OnPlayerModeSwitched?.Invoke(playerMode);
                }
            }
        }

        public void Run(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                speed = runSpeed;
            }
            if (context.canceled)
            {
                speed = walkSpeed;
            }
        }

        public void Reload(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                distanceAttack.ReloadCurrentWeapon();
            }
        }

        void Update()
        {
            if (playerMode != PlayerMode.normal)
            {
                return;
            }
            if (myPlayerControls.Infantry.Fire.IsPressed())
            {
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                mouseWorldPosition = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, 0);

                if (distanceAttack.CanFire())
                {
                    distanceAttack.Fire(mouseWorldPosition);
                }
            }
        }

    }

}