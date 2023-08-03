using TestAssignment.Core;
using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class FlyingEnemyComponent : BaseCharacterComponent
    {
        private CharacterStateMachine _stateMachine;

        private void Update()
        {
            var direction = Target.transform.position - transform.position;
            direction.y = 0;
            MovementDirection = direction;
        }

        private void Start()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var idleState = new IdleState();
            var movingState = new MovingState(this);
            var shootingState = new ShootingState(this);

            idleState.SetTransitions(new Transition(movingState, () => GameManager.Instance.GameStarted));
            movingState.SetTransitions(new Transition(shootingState, () => TargetIsVisible()));
            shootingState.SetTransitions(new Transition(movingState, () => !TargetIsVisible()));

            _stateMachine.Initialize(this, idleState, idleState, movingState, shootingState);

            Target = GameManager.Instance.Player;
        }

        private bool TargetIsVisible()
        {
            if (Target == null)
                return false;

            var ray = new Ray(transform.position, Target.transform.position - transform.position);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent<BaseCharacterComponent>(out _))
            {
                return true;
            }

            return false;
        }
    }
}