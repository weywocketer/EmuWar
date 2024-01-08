using UnityEngine;

namespace FubarOps.SquadManagement
{
    /// <summary>
    /// Contains general soldier information.
    /// </summary>
    public class SoldierData : MonoBehaviour
    {
        public string rank = "PVT";
        public string fullName = "Henry West";

        public override string ToString()
        {
            return $"{rank} {fullName}";
        }
    }
}
