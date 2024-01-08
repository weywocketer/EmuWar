using UnityEngine;

namespace FubarOps.Combat
{
    public class BloodSpawner : MonoBehaviour
    {
        [SerializeField] GameObject bloodPrefab;
        Transform hierarchyParent;

        void Awake()
        {
            GetComponent<Health>().OnHealthChanged += SpawnBlood;
            hierarchyParent = GameObject.FindGameObjectWithTag("BloodParent").transform;
        }

        void SpawnBlood()
        {
            Instantiate(bloodPrefab,transform.position, transform.rotation, hierarchyParent);
        }
    }
}
