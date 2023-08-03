using Newbeedev.ObjectsPool;
using TestAssignment.Characters;
using TestAssignment.Characters.Interfaces;
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
        public override void EnterState() { }
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
                var projectile = ObjectSpawnManager.Spawn(_shooting.Weapon.Projectile,
                    _shooting.Transform.position + Vector3.up + _shooting.Transform.right * 0.2f,
                    _shooting.Transform.rotation);

                projectile.Shot(_shooting, targetDirection);
                projectile.OnCharacterHit = HitTarget;
            }
        }

        private void HitTarget(BaseCharacterComponent target, IShooting attacker)
        {
            target.TakeDamage(attacker.Damage);
        }
    }
}
