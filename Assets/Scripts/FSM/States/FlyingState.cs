using TestAssignment.Characters.Interfaces;

namespace TestAssignment.FSM.States
{
    public class FlyingState : DistanceMovingState
    {
        private float _flyingHeight;

        public FlyingState(IDistanceMovable movable, float flyingHeight) : base(movable)
        {
            _flyingHeight = flyingHeight;
        }

        public override void EnterState()
        {
            base.EnterState();

            var transform = _movable.Rigidbody.transform;
            var position = transform.position;
            position.y = _flyingHeight;
            transform.position = position;
        }
    }
}
