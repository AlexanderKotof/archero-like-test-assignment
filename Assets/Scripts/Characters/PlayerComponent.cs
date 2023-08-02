using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class PlayerComponent : CharacterComponent
    {
        public static PlayerComponent Instance;

        private CharacterStateMachine _stateMachine;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var idleState = new IdleState();
            var movingState = new MovingState(this);
            var shootingState = new ShootingState(this);

            idleState.SetTransitions(new Transition(movingState, () => GameManager.GameStarted));
            movingState.SetTransitions(new Transition(shootingState, () => TargetIsVisible() && MovementDirection == Vector3.zero));
            shootingState.SetTransitions(new Transition(movingState, () => !TargetIsVisible() || MovementDirection != Vector3.zero));

            _stateMachine.Initialize(this, idleState, idleState, movingState, shootingState);
        }
        private void Update()
        {
            MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private bool TargetIsVisible()
        {
            if (Target == null)
                return false;

            var ray = new Ray(transform.position, Target.transform.position - transform.position);
            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent<CharacterComponent>(out _))
            {
                return true;
            }

            return false;
        }
    }
}