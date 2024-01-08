using UnityEngine;

namespace FubarOps.Movement
{
    public interface ISteering
    {
        Vector3 CalculateVelocity();
    }
}