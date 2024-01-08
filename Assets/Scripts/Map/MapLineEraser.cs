using UnityEngine;
using UnityEngine.EventSystems;

namespace FubarOps.Map
{
    public class MapLineEraser : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Destroy(gameObject);
            }
        }
    }
}