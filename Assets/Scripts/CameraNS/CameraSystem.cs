using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;
using FubarOps.Core;
using FubarOps.Constants;
using FubarOps.Movement;

namespace FubarOps.CameraNS
{
    public class CameraSystem : MonoBehaviour
    {
        [Header("Only pin the object you want to be\nfollowed to the followedTransform field.\nThere's no need to modify the virtual\ncameras, they are changed through this script.\n")]

        [SerializeField] Transform followedTransform;
        [SerializeField] float normalMovementSpeed = 50;
        [SerializeField] float quickMovementSpeed = 100;
        int pixelCameraZoomLevel = 2;
        int mapCameraZoomLevel = 2;
        float movementSpeed;
        Vector2 movementInput;
        PixelPerfectCamera pixelPerfectCamera;

        [SerializeField] CinemachineVirtualCamera virtualCameraFollowing;
        [SerializeField] CinemachineVirtualCamera virtualCameraBinoculars;
        [SerializeField] CinemachineVirtualCamera virtualCameraMap;

        void Awake()
        {
            movementSpeed = normalMovementSpeed;
            pixelPerfectCamera = GetComponentInChildren<PixelPerfectCamera>();
            virtualCameraFollowing.Follow = followedTransform;
        }

        void Start()
        {
            GameController.Instance.playerControlledSoldier.GetComponent<ManualSteering>().OnPlayerModeSwitched += SwitchCamera;
        }

        public void Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementInput = context.ReadValue<Vector2>();
            }
            if (context.canceled)
            {
                movementInput = Vector2.zero;
            }
        }

        public void ChangeSpeed(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementSpeed = quickMovementSpeed;
            }
            if (context.canceled)
            {
                movementSpeed = normalMovementSpeed;
            }
        }

        void SwitchCamera(PlayerMode playerMode)
        {
            switch (playerMode)
            {
                case PlayerMode.normal:
                    virtualCameraFollowing.Priority = 2;
                    virtualCameraBinoculars.Priority = 0;
                    virtualCameraMap.Priority = 1;
                    break;
                case PlayerMode.usingBinoculars:
                    virtualCameraBinoculars.transform.position = virtualCameraFollowing.transform.position; // Reset free vcam position to the following camera.
                    virtualCameraFollowing.Priority = 1;
                    virtualCameraBinoculars.Priority = 2;
                    virtualCameraMap.Priority = 0;
                    break;
                case PlayerMode.usingMap:
                    virtualCameraFollowing.Priority = 1;
                    virtualCameraBinoculars.Priority = 0;
                    virtualCameraMap.Priority = 2;
                    break;
            }
        }

        public void ChangeZoomLevel(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (pixelPerfectCamera.enabled)
                {
                    if (pixelCameraZoomLevel == 1)
                    {
                        pixelCameraZoomLevel = 4;
                    }
                    else
                    {
                        pixelCameraZoomLevel--;
                    }

                    pixelPerfectCamera.refResolutionX = 160 * pixelCameraZoomLevel;
                    pixelPerfectCamera.refResolutionY = 90 * pixelCameraZoomLevel;
                }
                else
                {
                    if (mapCameraZoomLevel == 1)
                    {
                        mapCameraZoomLevel = 4;
                    }
                    else
                    {
                        mapCameraZoomLevel--;
                    }

                    virtualCameraMap.m_Lens.OrthographicSize = 10 * mapCameraZoomLevel;
                }

            }
        }

        void LateUpdate()
        {
            if (virtualCameraBinoculars.Priority == 2)
            {
                virtualCameraBinoculars.transform.Translate(movementInput * movementSpeed * Time.deltaTime);

                // Clamp camera to map borders.
                virtualCameraBinoculars.transform.position = new Vector3(Mathf.Clamp(virtualCameraBinoculars.transform.position.x, 0, GameController.Instance.mapSize.x), Mathf.Clamp(virtualCameraBinoculars.transform.position.y, 0, GameController.Instance.mapSize.y), -10);
            }
            else if (virtualCameraMap.Priority == 2)
            {
                virtualCameraMap.transform.Translate(movementInput * movementSpeed * Time.deltaTime);
            }
        }
    }
}
