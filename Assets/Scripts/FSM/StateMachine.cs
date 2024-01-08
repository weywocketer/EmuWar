using System;
using UnityEngine;
using FubarOps.Constants;
using FubarOps.BoidNS;

namespace FubarOps.FSM
{
    [Serializable]
    public class StateMachine : MonoBehaviour
    {
        public State currentState { get; private set; }
        [SerializeField] InitialState initialState;

        // FSM initialization has to happen in Start() (after Boid's Awake)
        void Start()
        {
            switch (initialState)
            {
                case InitialState.emuWander:
                    currentState = new EmuWanderState(GetComponent<Emu>());
                    break;
                case InitialState.emuFlockToTarget:
                    currentState = new EmuFlockToTargetState(GetComponent<Emu>());
                    break;
                case InitialState.soldierWander:
                    //currentState = new SoldierWanderState(GetComponent<Boid>());
                    break;
            }

            if (currentState != null)
            {
                currentState.Enter();
            }
        }

        void FixedUpdate()
        {
            currentState.Execute();
        }

        public void ChangeState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();
        }
    }
}