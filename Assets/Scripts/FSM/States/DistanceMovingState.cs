using TestAssignment.Characters.Interfaces;
using UnityEngine;

namespace TestAssignment.FSM.States
{
    public class DistanceMovingState : State
    {
        protected IDistanceMovable _movable;
        private float _distance;

        public DistanceMovingState(IDistanceMovable movable)
        {
            _movable = movable;
        }

        public override void EnterState()
        {
            _distance = 0;
            _movable.MovementDirection = GenerateRandomDirection();
        }

        public override void ExitState()
        {
            _movable.Rigidbody.velocity = Vector3.zero;
        }

        public override void UpdateState()
        {
            _movable.Rigidbody.velocity = _movable.MovementDirection * _movable.MoveSpeed;

            if (_movable.MovementDirection != Vector3.zero)
                _movable.Rigidbody.transform.rotation = Quaternion.LookRotation(_movable.MovementDirection);

            _distance += _movable.MoveSpeed * Time.deltaTime;
        }

        private Vector3 GenerateRandomDirection()
        {
            var vector = Random.onUnitSphere;
            vector.y = 0;
            vector.Normalize();
            return vector;
        }

        public bool DistancePassed()
        {
            return _distance >= _movable.MovingDistance;
        }
    }
}
