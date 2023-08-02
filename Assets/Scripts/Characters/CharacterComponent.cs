using TestAssignment.Characters.Interfaces;
using TestAssignment.Weapons;
using UnityEngine;

namespace TestAssignment.Characters
{
    public abstract class CharacterComponent : MonoBehaviour, IHealth, IMovable, IShooting
    {
        [SerializeField]
        private float _startHealth;
        private float _currentHealth;
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
        public float CurrentHealth => _currentHealth;
        public bool IsDied => _currentHealth <= 0;
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
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                CharacterDied?.Invoke(this);
            }
        }
    }
}
