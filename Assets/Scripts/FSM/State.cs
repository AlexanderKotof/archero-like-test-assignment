using System;
using TestAssignment.FSM.Transitions;

namespace TestAssignment.FSM.States
{
    public abstract class State
    {
        private Transition[] _transitions;

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public void SetTransitions(params Transition[] transitions)
        {
            _transitions = transitions;
        }

        public bool ShooldChangeState(out State state)
        {
            foreach (var transition in _transitions)
            {
                if (transition.trigger.Invoke())
                {
                    state = transition.toState;
                    return true;
                }
            }
            state = default;
            return false;
        }
    }
}