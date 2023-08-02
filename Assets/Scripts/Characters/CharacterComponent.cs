using TestAssignment.Characters.Interfaces;
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
        private float _shootingSpeed;
        [SerializeField]
        private float _damage;
        [SerializeField]
        private Weapon _weapon;

        [SerializeField]
        private Rigidbody _rigidbody;

        public float StartHealth => _startHealth;
        public float CurrentHealth => _currentHealth;
        public bool IsDied => _currentHealth <= 0;
        public float MoveSpeed => _moveSpeed;
        public float ShootingSpeed => _shootingSpeed;
        public float Damage => _damage;
        public IHealth.OnDied CharacterDied;
        public CharacterComponent Target { get; set; }
        public Vector3 MovementDirection { get; protected set; }

        public Weapon Weapon => _weapon;
        public Rigidbody Rigidbody => _rigidbody;

        public void TakeDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                CharacterDied?.Invoke(this);
            }
        }

        public abstract void Move(Vector3 direction);
    }
}
