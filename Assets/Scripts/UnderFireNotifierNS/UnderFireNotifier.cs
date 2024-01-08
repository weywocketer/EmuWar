using UnityEngine;
using FubarOps.Core;
using FubarOps.Constants;
using FubarOps.Combat;
using FubarOps.GridNS;
using FubarOps.FSM;
using FubarOps.BoidNS;

namespace FubarOps.UnderFireNotifierNS
{
    /// <summary>
    /// Used to change state of all nearby Emus to EmuDecideState, when the gameObject has performed a distance attack.
    /// </summary>
    [RequireComponent(typeof(DistanceAttack))]
    [RequireComponent(typeof(GridPositionTracker))]
    public class UnderFireNotifier : MonoBehaviour
    {
        DistanceAttack distanceAttack;
        GridPositionTracker gridPositionTracker;

        void Awake()
        {
            distanceAttack = GetComponent<DistanceAttack>();
            gridPositionTracker = GetComponent<GridPositionTracker>();
        }

        void OnEnable()
        {
            distanceAttack.OnWeaponFired += NotifyNearbyEmus;
        }

        void NotifyNearbyEmus(float notUsedParameter)
        {

            foreach (GameObject emu in gridPositionTracker.GetListOfNearbyBoids(GameController.Instance.emusGrid))
            {
                StateMachine emuStateMachine = emu.GetComponent<StateMachine>();

                if (Vector2.SqrMagnitude(emu.transform.position - transform.position) <= AttackRangeSquaredValues.longRange)
                {
                    if (emuStateMachine.currentState is EmuWanderState || emuStateMachine.currentState is EmuFlockToTargetState)
                    {
                        emuStateMachine.ChangeState(new EmuDecideState(emu.GetComponent<Emu>(), transform));
                    }
                }
            }
        }

    }
}
