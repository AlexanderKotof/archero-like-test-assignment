using TestAssignment.Characters.Interfaces;
using UnityEngine;

namespace TestAssignment.FSM.States
{
    public class ShootingState : State
    {
        private IShooting _shooting;
        private float _timer;

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

            if (_timer > 1 / _shooting.ShootingSpeed)
            {
                _timer = 0;
                Debug.Log("Shoot!");
            }
        }
    }
}
