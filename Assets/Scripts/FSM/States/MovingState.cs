using TestAssignment.Characters.Interfaces;
using UnityEngine;

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
            _movable.Rigidbody.velocity = Vector3.zero;
        }

        public override void ExitState()
        {
            _movable.Rigidbody.velocity = Vector3.zero;
        }

        public override void UpdateState()
        {
            _movable.Rigidbody.velocity += _movable.MovementDirection * _movable.MoveSpeed;
            _movable.Rigidbody.velocity = Vector3.ClampMagnitude(_movable.Rigidbody.velocity, _movable.MoveSpeed);

            if (_movable.MovementDirection != Vector3.zero )
                _movable.Rigidbody.transform.rotation = Quaternion.LookRotation(_movable.MovementDirection);
        }
    }
}
