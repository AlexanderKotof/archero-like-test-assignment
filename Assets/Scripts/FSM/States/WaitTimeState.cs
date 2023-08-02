using UnityEngine;

namespace TestAssignment.FSM.States
{
    public class WaitTimeState : State
    {
        private float _waitTime;
        private float _timer;
        public WaitTimeState(float waitTime)
        {
            _waitTime = waitTime;
        }

        public override void EnterState()
        {
            _timer = 0;
        }

        public override void ExitState() { }

        public override void UpdateState()
        {
            _timer += Time.deltaTime;
        }

        public bool WaitIsOver()
        {
            return _timer >= _waitTime;
        }
    }
}
