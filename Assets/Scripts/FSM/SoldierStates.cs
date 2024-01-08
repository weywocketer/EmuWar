using UnityEngine;
using FubarOps.Core;
using FubarOps.Movement;
using FubarOps.Combat;
using FubarOps.BoidNS;
using FubarOps.GridNS;
using FubarOps.Constants;

namespace FubarOps.FSM
{
    // This file contains state classes for the usage of Soldier class.
    // State naming convention: Soldier[StateName]State

    public abstract class SoldierState : State
    {
        protected Boid context;
        protected AiSteering aiSteering;
        protected GridPositionTracker gridPositionTracker;
        protected StateMachine stateMachine;
        protected DistanceAttack distanceAttack;
        protected DelayedConditionTimer delayedConditionTimer;

        public SoldierState(Boid _context)
        {
            context = _context;
            aiSteering = context.steering as AiSteering;
            gridPositionTracker = context.gridPositionTracker;
            stateMachine = context.GetComponent<StateMachine>();
            distanceAttack = context.GetComponent<DistanceAttack>();
            delayedConditionTimer = context.GetComponent<DelayedConditionTimer>();
        }

        //public override void Enter() { }

        public override void Execute()
        {

            // Choose target and fire.
            if (Time.time >= delayedConditionTimer.nextDistanceCheckTime)
            {

                if (distanceAttack.CanFire())
                {
                    Transform chosenTarget = null;
                    float distanceToChosenTarget = float.MaxValue;

                    foreach (GameObject emu in gridPositionTracker.GetListOfNearbyBoids(GameController.Instance.emusGrid))
                    {
                        float distanceToCurrent = context.GetSquaredDistanceTo(emu.transform);

                        if (distanceToCurrent <= distanceAttack.attackRangeSquared)
                        {
                            if (distanceToCurrent < distanceToChosenTarget)
                            {
                                chosenTarget = emu.transform;
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
                        distanceAttack.Fire(chosenTarget.position);
                    }
                }

            }
        }

    }

    public class SoldierWanderState : SoldierState
    {
        public SoldierWanderState(Boid _context) : base(_context) { }

        public override void Enter()
        {
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuFlockWander);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit() { }
    }

    public class SoldierFollowLeaderState : SoldierState
    {
        public SoldierFollowLeaderState(Boid _context, Transform squadDirection, Vector2 positionInFormation) : base(_context)
        {
            aiSteering.TargetTransform = squadDirection;
            aiSteering.positionInFormation = positionInFormation;
        }

        public override void Enter()
        {
            aiSteering.SetBehaviourWeights(BehaviourWeights.soldierOffsetPursuit);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit() { }
    }

    public class SoldierMoveToState : SoldierState
    {
        public SoldierMoveToState(Boid _context, Vector2 destination) : base(_context)
        {
            aiSteering.TargetTransform = null;
            aiSteering.targetPosition = destination;
        }

        public override void Enter()
        {
            aiSteering.SetBehaviourWeights(BehaviourWeights.emuSeek);
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Exit() { }
    }


}
