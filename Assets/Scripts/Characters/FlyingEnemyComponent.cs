using TestAssignment.Characters.Interfaces;
using TestAssignment.Core;
using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using TestAssignment.Utils;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class FlyingEnemyComponent : BaseCharacterComponent, IRewardable, IDistanceMovable
    {
        private CharacterStateMachine _stateMachine;

        [SerializeField]
        private float _movingDistance;
        [SerializeField]
        private float _flyingHeight;
        [SerializeField]
        private int _minReward;
        [SerializeField]
        private int _maxReward;
        [SerializeField]
        private float _waitingTime;

        public float MovingDistance => _movingDistance;

        public int GetReward() => Random.Range(_minReward, _maxReward);

        // setup state machine
        private void Awake()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var awaitStartState = new IdleState();
            var waitingState = new WaitTimeState(_waitingTime);
            var movingState = new FlyingState(this, _flyingHeight);
            var shootingState = new ShootingState(this);

            awaitStartState.SetTransitions(new Transition(movingState, () => GameManager.Instance.GameStarted));
            waitingState.SetTransitions(
                new Transition(movingState, waitingState.WaitIsOver),
                new Transition(shootingState, () => ShootingUtils.TargetIsVisible(this, Target))
                );
            movingState.SetTransitions(
                new Transition(shootingState, () => ShootingUtils.TargetIsVisible(this, Target)),
                new Transition(waitingState, movingState.DistancePassed)
                );
            shootingState.SetTransitions(new Transition(movingState, () => !ShootingUtils.TargetIsVisible(this, Target)));

            _stateMachine.Initialize(this, awaitStartState, awaitStartState, movingState, shootingState);
        }

        // when enemy respawned set waitForStart state and restore full health
        private void OnEnable()
        {
            RestoreHealth(StartHealth);
            _stateMachine.SetDefaultState();
        }
    }
}