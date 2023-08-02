using System;
using TestAssignment.FSM.States;

namespace TestAssignment.FSM.Transitions
{
    public class Transition
    {
        public State toState;
        public Func<bool> trigger;

        public Transition(State toState, Func<bool> trigger)
        {
            this.toState = toState;
            this.trigger = trigger;
        }
    }
}
