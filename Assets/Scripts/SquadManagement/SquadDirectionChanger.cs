using UnityEngine;

namespace FubarOps.SquadManagement
{
    public class SquadDirectionChanger : MonoBehaviour
    {
        public Vector3 eulerRotation;

        void FixedUpdate()
        {
            transform.eulerAngles = eulerRotation;
        }
    }
}