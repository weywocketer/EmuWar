using UnityEngine;
using UnityEngine.EventSystems;

namespace FubarOps.Map
{
    public class DrawColorPicker : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] Color color;
        MapLineDrawer mapLineDrawer;

        void Awake()
        {
            mapLineDrawer = GetComponentInParent<MapLineDrawer>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                mapLineDrawer.lineColor = color;
            }
        }

        void OnValidate()
        {
            GetComponent<SpriteRenderer>().color = color;
        }
    }
}