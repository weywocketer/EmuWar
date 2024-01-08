using UnityEngine;
using FubarOps.Core;
using FubarOps.Movement;
using FubarOps.Combat;
using FubarOps.BoidNS;
using FubarOps.GridNS;
using FubarOps.Constants;

namespace FubarOps.FSM
{
    // This file contains state classes for the usage of Emu class.
    // State naming convention: Emu[StateName]State

    public abstract class EmuMovementState : State
    {
        protected Emu context;
        protected AiSteering aiSteering;
        protected GridPositionTracker gridPositionTracker;
        protected StateMachine stateMachine;
        protected DelayedConditionTimer delayedConditionTimer;

        public EmuMovementState(Emu _context)
        {
            context = _context;
            aiSteering = context.steering as AiSteering;
            gridPositionTracker = context.gridPositionTracker;
            stateMachine = context.GetComponent<StateMachine>();
            delayedConditionTimer = context.GetComponent<DelayedConditionTimer>();
        }

        public override void Execute()
        {
            if (Time.time >= delayedConditionTimer.nextDistanceCheckTime)
            {
                Transform chosenTarget = null;
                float distanceToChosenTarget = float.MaxValue;

                foreach (GameObject soldier in gridPositionTracker.GetListOfNearbyBoids(GameController.Instance.soldiersGrid))
                {
                    float distanceToCurrent = context.GetSquaredDistanceTo(soldier.transform);

                    if (distanceToCurrent <= AttackRangeSquaredValues.normalRange)
                    {
                        if (distanceToCurrent < distanceToChosenTarget)
                        {
                            chosenTarget = soldier.transform;
                            distanceToChosenTarget = distanceToCurrent;
                        }
                    }
                }

                if (chosenTarget == null)
                {
                    // CheckTime is increased only if there are no targets in range.
                    delayedConditionTimer.SetDistanceCheckTime();
                }
                else
                {
                    stateMachine.ChangeState(new EmuDecideState(context, chosenTarget));
                }
            }

        }

    }


    public class EmuWanderState : EmuMovementState
    {
        public EmuWanderState(Emu _context) : base(_context) { }

        public override void Enter()
        {
            aiSteering.maxSpeed = EmuSpeed.walking;
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuFlockWander);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit() { }
    }

    public class EmuFlockToTargetState : EmuMovementState
    {
        public EmuFlockToTargetState(Emu _context) : base(_context) { }

        public override void Enter()
        {
            aiSteering.TargetTransform = context.destinations[0];
            aiSteering.maxSpeed = EmuSpeed.walking;
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuSeek);
        }

        public override void Execute()
        {
            base.Execute();

            if (context.GetSquaredDistanceTo(aiSteering.TargetTransform) <= Mathf.Pow(10, 2))
            {
                aiSteering.TargetTransform = null;
                stateMachine.ChangeState(new EmuWanderState(context));
            }
        }

        public override void Exit() { }
    }

    public class EmuDecideState : State
    {
        Emu context;
        AiSteering aiSteering;
        StateMachine stateMachine;

        public EmuDecideState(Emu _context, Transform chosenTarget)
        {
            context = _context;
            aiSteering = context.steering as AiSteering;
            stateMachine = context.gameObject.GetComponent<StateMachine>();

            aiSteering.TargetTransform = chosenTarget;
        }

        public override void Enter()
        {
            if (UnityEngine.Random.Range(0, 3) == 0)
            {
                stateMachine.ChangeState(new EmuFleeState(context));
            }
            else
            {
                stateMachine.ChangeState(new EmuAttackState(context));
            }
        }

        public override void Execute() { }

        public override void Exit() { }
    }

    public class EmuAttackState : State
    {
        Emu context;
        AiSteering aiSteering;
        StateMachine stateMachine;

        public EmuAttackState(Emu _context)
        {
            context = _context;
            aiSteering = context.steering as AiSteering;
            stateMachine = context.GetComponent<StateMachine>();
        }

        public override void Enter()
        {
            aiSteering.maxSpeed = EmuSpeed.running;
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuSeek);
        }

        public override void Execute()
        {
            if (aiSteering.TargetTransform == null)
            {
                stateMachine.ChangeState(new EmuWanderState(context));
            }
        }

        public override void Exit()
        {

        }
    }

    public class EmuFleeState : State
    {
        Emu context;
        AiSteering aiSteering;
        StateMachine stateMachine;

        public EmuFleeState(Emu _context)
        {
            context = _context;
            aiSteering = context.steering as AiSteering;
            stateMachine = context.GetComponent<StateMachine>();
        }

        public override void Enter()
        {
            aiSteering.maxSpeed = EmuSpeed.running;
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuFlee);
        }

        public override void Execute()
        {
            if (aiSteering.TargetTransform == null)
            {
                stateMachine.ChangeState(new EmuWanderState(context));
            }
        }

        public override void Exit()
        {

        }
    }




}
