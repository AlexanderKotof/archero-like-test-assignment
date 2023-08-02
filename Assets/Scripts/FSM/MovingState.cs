using TestAssignment.Characters.Interfaces;

namespace TestAssignment.FSM.States
{
    public class MovingState : State
    {
        private IMovable _movable;

        public MovingState(IMovable movable)
        {
            _movable = movable;
        }

        public override void EnterState()
        {

        }

        public override void ExitState() { }

        public override void UpdateState()
        {
            _movable.Rigidbody.velocity = _movable.MovementDirection * _movable.MoveSpeed;
        }
    }
}
