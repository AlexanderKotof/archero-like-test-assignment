using TestAssignment.Characters.Interfaces;
using TestAssignment.Weapons;
using UnityEngine;

namespace TestAssignment.Characters
{
    public abstract class CharacterComponent : MonoBehaviour, IHealth, IMovable, IShooting
    {
        [SerializeField]
        private float _startHealth;
        [SerializeField]
        private float _moveSpeed;
        [SerializeField]
        private float _rotationSpeed;
        [SerializeField]
        private Weapon _weapon;
        [SerializeField]
        private Rigidbody _rigidbody;

        public IHealth.OnDied CharacterDied;
        public float StartHealth => _startHealth;
        public float CurrentHealth { get; private set; }
        public bool IsDied => CurrentHealth <= 0;
        public float MoveSpeed => _moveSpeed;
        public float RotationSpeed => _rotationSpeed;
        public float ShootingSpeed => _weapon.ShootingSpeed;
        public float Damage => _weapon.Damage;
        public CharacterComponent Target { get; set; }
        public Vector3 MovementDirection { get; set; }
        public Weapon Weapon => _weapon;
        public Rigidbody Rigidbody => _rigidbody;
        public Transform Transform => transform;

        public void TakeDamage(float damage)
        {
            CurrentHealth -= damage;

            if (CurrentHealth <= 0)
            {
                CharacterDied?.Invoke();
            }
        }

        public void RestoreHealth()
        {
            CurrentHealth = _startHealth;
        }

        public void SetTarget(CharacterComponent target)
        {
            Target = target;
        }
    }
}
