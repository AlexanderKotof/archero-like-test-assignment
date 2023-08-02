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
                new Transition(shootingState, () => ShootingUtils.TargetIsVisible(this, Target))
                );
            movingState.SetTransitions(
                new Transition(shootingState, () => ShootingUtils.TargetIsVisible(this, Target)),
                new Transition(waitingState, movingState.DistancePassed)
                );
            shootingState.SetTransitions(new Transition(movingState, () => !ShootingUtils.TargetIsVisible(this, Target)));

            _stateMachine.Initialize(this, awaitStartState, awaitStartState, waitingState, movingState, shootingState);

            Target = GameManager.Instance.Player;
        }
    }
}