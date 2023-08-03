﻿using Newbeedev.ObjectsPool;
using TestAssignment.Characters;
using TestAssignment.Characters.Interfaces;
using UnityEngine;

namespace TestAssignment.Weapons
{
    public class ProjectileComponent : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private ParticleSystem _hitParticles;

        private Vector3 _direction;
        public IShooting Attacker { get; private set; }

        public delegate void CharacterHitHandler(BaseCharacterComponent target, IShooting attacker);
        public CharacterHitHandler OnCharacterHit;

        public void Shot(IShooting attacker, Vector3 direction)
        {
            Attacker = attacker;
            _direction = direction;
        }

        private void Update()
        {
            _rigidbody.velocity = _direction * _speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ProjectileComponent>(out _))
            {
                return;
            }

            if (other.TryGetComponent<BaseCharacterComponent>(out var target))
            {
                if (target == (BaseCharacterComponent)Attacker)
                    return;

                OnCharacterHit?.Invoke(target, Attacker);
            }

            ObjectSpawnManager.Despawn(this);

            if (_hitParticles != null)
            {
                var particles = ObjectSpawnManager.Spawn(_hitParticles, transform.position, Quaternion.identity);
                ObjectSpawnManager.DespawnAfter(particles, particles.main.duration);
            }
        }
    }
}
