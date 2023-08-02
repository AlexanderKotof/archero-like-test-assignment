using TestAssignment.Characters;
using TestAssignment.FSM.States;
using UnityEngine;

namespace TestAssignment.FSM
{
    public class CharacterStateMachine : MonoBehaviour
    {
        public CharacterComponent Character { get; private set; }
        public State CurrentState { get; private set; }

        private State[] _states;

        public void Initialize(CharacterComponent character, State defaultState, params State[] states)
        {
            _states = states;
            Character = character;
            SwitchState(defaultState);
        }

        protected void SwitchState(State newState)
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
    }
}
