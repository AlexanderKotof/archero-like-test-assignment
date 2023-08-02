using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class SimpleEnemyComponent : CharacterComponent
    {
        private CharacterStateMachine _stateMachine;

        public override void Move(Vector3 direction)
        {
            transform.Translate(direction * MoveSpeed * Time.deltaTime);
        }

        private void Update()
        {
            MovementDirection = Vector3.right;
        }

        private void Start()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var idleState = new IdleState();
            var movingState = new MovingState(this);
            var shootingState = new ShootingState(this);

            idleState.SetTransitions(new Transition(movingState, () => GameManager.GameStarted));
            movingState.SetTransitions(new Transition(shootingState, () => TargetIsVisible()));
            shootingState.SetTransitions(new Transition(movingState, () => !TargetIsVisible()));

            _stateMachine.Initialize(this, idleState, idleState, movingState, shootingState);

            Target = PlayerComponent.Instance;
        }

        private bool TargetIsVisible()
        {
            if (Target == null)
                return false;

            var ray = new Ray(transform.position + Vector3.up, Target.transform.position - transform.position + Vector3.up);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent<CharacterComponent>(out _))
            {
                return true;
            }

            return false;
        }
    }
}