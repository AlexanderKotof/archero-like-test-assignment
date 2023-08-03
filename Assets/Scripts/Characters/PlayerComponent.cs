using TestAssignment.Core;
using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using TestAssignment.Input;
using TestAssignment.Utils;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class PlayerComponent : BaseCharacterComponent
    {
        private CharacterStateMachine _stateMachine;
        private WaitTimeState _uncontrollableState;

        private const float _uncontrollableTime = 0.2f;
        private const float _pushForceModifier = 500f;

        // setup state machine
        private void Awake()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var waitForStartState = new IdleState();
            var standingState = new IdleState();
            var movingState = new MovingState(this);
            var shootingState = new ShootingState(this);
            _uncontrollableState = new WaitTimeState(_uncontrollableTime);

            waitForStartState.SetTransitions(new Transition(standingState, () => GameManager.Instance.GameStarted));
            standingState.SetTransitions(
                new Transition(movingState, () => MovementDirection != Vector3.zero),
                new Transition(shootingState, () => Target != null )
                );
            movingState.SetTransitions(new Transition(standingState, () => MovementDirection == Vector3.zero));
            shootingState.SetTransitions(
                new Transition(standingState, () => Target == null),
                new Transition(movingState, () => MovementDirection != Vector3.zero)
                );
            _uncontrollableState.SetTransitions(new Transition(standingState, _uncontrollableState.WaitIsOver));

            _stateMachine.Initialize(this, waitForStartState, waitForStartState, standingState, movingState, shootingState, _uncontrollableState);

            RestoreHealth();

            InputManager.MovementInput += OnMovementInput;
        }

        private void OnDestroy()
        {
            InputManager.MovementInput -= OnMovementInput;
        }

        private void OnMovementInput(Vector2 obj)
        {
            MovementDirection = new Vector3(obj.x, 0, obj.y);
        }

        // when player enters new level set waitForStart state
        private void OnEnable()
        {
            _stateMachine.SetDefaultState();
        }

        private void Update()
        {
            if (!GameManager.Instance.GameStarted)
                return;

            Target = GetNearestVisibleEnemy();
        }

        private BaseCharacterComponent GetNearestVisibleEnemy()
        {
            const float _distanceThreashold = 1f;

            var distance = float.MaxValue;
            BaseCharacterComponent nearest = null;

            foreach (var enemy in GameManager.Instance.SpawnedEnemies)
            {
                if (!ShootingUtils.TargetIsVisible(this, enemy))
                    continue;

                var distanceToEnemy = (enemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy + _distanceThreashold < distance)
                {
                    nearest = enemy;
                    distance = distanceToEnemy;
                }
            }
            return nearest;
        }

        // when player collides with enemy apply opposite force and take damage
        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.TryGetComponent<BaseCharacterComponent>(out _))
                return;

            // to prevent absorption of push impulse by walking, make the player uncontrollable
            _stateMachine.SwitchState(_uncontrollableState);

            var direction = transform.position - collision.collider.transform.position;
            Rigidbody.AddForce(direction.normalized * _pushForceModifier);

            TakeDamage(1);
        }
    }
}