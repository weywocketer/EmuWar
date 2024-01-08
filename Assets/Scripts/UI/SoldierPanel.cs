using FubarOps.Combat;
using FubarOps.SquadManagement;
using TMPro;
using UnityEngine;

namespace FubarOps.UI
{
    public class SoldierPanel : MonoBehaviour
    {
        TextMeshProUGUI textField;

        GameObject observedObject = null;
        Health observedHealth;
        Transform observedTransform;
        SoldierData observedSoldierData;
        DistanceAttack observedWeapons;

        void Awake()
        {
            textField = GetComponentInChildren<TextMeshProUGUI>();
        }

        void Start()
        {
            gameObject.SetActive(false);
        }

        public void FollowSoldier(GameObject soldier)
        {
            // Unsubscribe to old object.
            if (observedObject != null)
            {
                observedHealth.OnHealthChanged -= UpdateUI;
            }

            observedObject = soldier;

            if (observedObject != null)
            {
                observedHealth = observedObject.GetComponent<Health>();
                observedHealth.OnHealthChanged += UpdateUI;
                observedTransform = observedObject.transform;
                observedSoldierData = observedObject.GetComponent<SoldierData>();
                observedWeapons = observedObject.GetComponent<DistanceAttack>();
                UpdateUI();
            }

            gameObject.SetActive(observedObject != null);
        }

        void UpdateUI()
        {
            textField.text = $"{observedSoldierData}\n" +
                             $"{observedWeapons}";
        }

        void LateUpdate()
        {
            if (observedObject == null)
            {
                return;
            }

            Vector2 observedScreenPosition = Camera.main.WorldToScreenPoint(observedTransform.position);
            transform.position = observedScreenPosition + new Vector2(0, -100);
        }
    }
}
