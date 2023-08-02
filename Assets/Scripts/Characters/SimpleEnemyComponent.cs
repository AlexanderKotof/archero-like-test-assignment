using TestAssignment.Characters.Interfaces;
using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class SimpleEnemyComponent : CharacterComponent, IDistanceMovable
    {
        [SerializeField]
        private float _waitingTime;

        [SerializeField]
        private float _movingDistance;

        private CharacterStateMachine _stateMachine;
        public float MovingDistance => _movingDistance;

        private void Start()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var awaitStartState = new IdleState();
            var waitingState = new WaitTimeState(_waitingTime);
            var movingState = new DistanceMovingState(this);
            var shootingState = new ShootingState(this);

            awaitStartState.SetTransitions(new Transition(movingState, () => GameManager.GameStarted));
            waitingState.SetTransitions(
                new Transition(movingState, waitingState.WaitIsOver),
                new Transition(shootingState, TargetIsVisible)
                );
            movingState.SetTransitions(
                new Transition(shootingState, TargetIsVisible),
                new Transition(waitingState, movingState.DistancePassed)
                );
            shootingState.SetTransitions(new Transition(movingState, () => !TargetIsVisible()));

            _stateMachine.Initialize(this, awaitStartState, awaitStartState, waitingState, movingState, shootingState);

            Target = GameManager.Instance.Player;
        }

        private bool TargetIsVisible()
        {
            if (Target == null)
                return false;

            var ray = new Ray(transform.position + Vector3.up, Target.transform.position - transform.position + Vector3.up);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent<CharacterComponent>(out var target) && Target == target)
            {
                return true;
            }

            return false;
        }
    }
}