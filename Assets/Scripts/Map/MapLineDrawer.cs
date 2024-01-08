using UnityEngine;
using UnityEngine.InputSystem;
using FubarOps.Core;
using FubarOps.Movement;
using FubarOps.Constants;

namespace FubarOps.Map
{
    public class MapLineDrawer : MonoBehaviour
    {
        public bool isDrawingModeOn = false;
        [SerializeField] GameObject mapLinePrefab;
        MapLine currentLine = null;
        public Color lineColor = new Color32(24, 59, 128, 255);
        public float lineWidth = 0.5f;

        void Start()
        {
            GameController.Instance.playerControlledSoldier.GetComponent<ManualSteering>().OnPlayerModeSwitched += ToggleDrawingMode;
        }

        void ToggleDrawingMode(PlayerMode playerMode)
        {
            if (playerMode == PlayerMode.usingMap)
            {
                isDrawingModeOn = true;
            }
            else
            {
                isDrawingModeOn = false;
            }
        }

        void Update()
        {
            if (!isDrawingModeOn)
            {
                return;
            }
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                currentLine = Instantiate(mapLinePrefab, gameObject.transform).GetComponent<MapLine>();
                currentLine.SetLineParameters(lineColor, lineWidth);
            }
            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                currentLine.DestroyIfTooShort();
                currentLine = null;
            }
            if (currentLine != null)
            {
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                currentLine.AddPoint(transform.InverseTransformPoint(mouseWorldPosition));
            }
        }
    }
}
