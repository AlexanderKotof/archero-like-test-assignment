using TestAssignment.FSM;
using TestAssignment.FSM.States;
using TestAssignment.FSM.Transitions;
using UnityEngine;

namespace TestAssignment.Characters
{
    public class PlayerComponent : CharacterComponent
    {
        private CharacterStateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = GetComponent<CharacterStateMachine>();

            var idleState = new IdleState();
            var movingState = new MovingState(this);
            var shootingState = new ShootingState(this);

            idleState.SetTransitions(new Transition(movingState, () => GameManager.GameStarted));
            movingState.SetTransitions(new Transition(shootingState, () => Target != null && MovementDirection == Vector3.zero));
            shootingState.SetTransitions(new Transition(movingState, () => Target == null || MovementDirection != Vector3.zero));

            _stateMachine.Initialize(this, idleState, idleState, movingState, shootingState);
        }

        private void Update()
        {
            if (!GameManager.GameStarted)
                return;

            Target = GameManager.Instance.GetNearestEnemy();
            MovementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.collider.TryGetComponent<CharacterComponent>(out var other))
                return;

            var direction = transform.position - collision.collider.transform.position;
            Rigidbody.AddForce(direction.normalized * 200f);
            TakeDamage(1);
        }
    }
}