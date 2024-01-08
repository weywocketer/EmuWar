using UnityEngine;
using UnityEngine.EventSystems;

namespace FubarOps.Map
{
    public class DrawWidthPicker : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] float widthIncrement = 0.1f;
        MapLineDrawer mapLineDrawer;

        void Awake()
        {
            mapLineDrawer = GetComponentInParent<MapLineDrawer>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                mapLineDrawer.lineWidth = Mathf.Clamp(mapLineDrawer.lineWidth + widthIncrement, 0.1f, 1);
            }
        }
    }
}