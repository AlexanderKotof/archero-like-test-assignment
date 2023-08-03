using System;
using TestAssignment.Characters;
using TestAssignment.FSM.States;
using UnityEngine;

namespace TestAssignment.FSM
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public CharacterComponent Character { get; private set; }

        private State _defaultState;

        public State CurrentState { get; private set; }

        private State[] _states;

        public void Initialize(CharacterComponent character, State defaultState, params State[] states)
        {
            _states = states;
            _defaultState = defaultState;

            Character = character;

            SwitchState(defaultState);
        }

        public void SwitchState(State newState)
        {
            if (CurrentState != null)
                CurrentState.ExitState();

            CurrentState = newState;
            CurrentState.EnterState();
        }

        private void Update()
        {
            if (CurrentState.ShooldChangeState(out var newState))
            {
                SwitchState(newState);
            }

            CurrentState.UpdateState();
        }

        public void SetDefaultState()
        {
            SwitchState(_defaultState);
        }
    }
}
