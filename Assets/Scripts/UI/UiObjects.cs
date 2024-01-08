using UnityEngine;

namespace FubarOps.UI
{
    public class UiObjects : MonoBehaviour
    {
        public static UiObjects Instance { get; private set; }
        public SoldierPanel soldierPanel;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError("UiObjects already exists!");
            }
        }

    }
}
