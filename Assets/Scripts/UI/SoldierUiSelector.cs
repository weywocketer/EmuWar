using UnityEngine;
using UnityEngine.EventSystems;

namespace FubarOps.UI
{
    /// <summary>
    /// Displays soldier's panel when mouse is hovered over the unit.
    /// </summary>
    public class SoldierUiSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            UiObjects.Instance.soldierPanel.FollowSoldier(gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UiObjects.Instance.soldierPanel.FollowSoldier(null);
        }
    }
}
