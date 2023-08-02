﻿using TestAssignment.Characters.Interfaces;
using UnityEngine;

namespace TestAssignment.FSM.States
{
    public class ShootingState : State
    {
        private IShooting _shooting;
        private float _timer;

        private const float _dotProductTreashold = 0.9f;

        public ShootingState(IShooting shooting)
        {
            _shooting = shooting;
        }

        public override void EnterState()
        {
            _timer = 0;
        }

        public override void ExitState() { }

        public override void UpdateState()
        {
            _timer += Time.deltaTime;

            var targetDirection = _shooting.Target.Transform.position - _shooting.Transform.position;
            targetDirection = targetDirection.normalized;

            _shooting.Transform.rotation = Quaternion.Lerp(_shooting.Transform.rotation,
                Quaternion.LookRotation(targetDirection),
                _shooting.RotationSpeed * Time.deltaTime);

            var dotProduct = Vector3.Dot(_shooting.Transform.forward, targetDirection);
            if (dotProduct < _dotProductTreashold)
                return;

            if (_timer > 1 / _shooting.ShootingSpeed)
            {
                _timer = 0;
                Debug.Log($"Shoot at {_shooting.Target}!");
            }
        }
    }
}
