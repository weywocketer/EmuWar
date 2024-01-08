using UnityEngine;

namespace FubarOps.Combat
{
    /// <summary>
    /// Used by some States to perform distance checks in fixed intervals.
    /// </summary>
    public class DelayedConditionTimer : MonoBehaviour
    {
        [SerializeField] float maxCheckDelay = 5f;
        public float nextDistanceCheckTime { get; private set; }

        void Start()
        {
            SetDistanceCheckTime();
        }

        public void SetDistanceCheckTime()
        {
            nextDistanceCheckTime = Time.time + UnityEngine.Random.Range(0f, maxCheckDelay);
        }
    }
}
