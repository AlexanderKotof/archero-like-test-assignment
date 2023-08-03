using TestAssignment.Weapons;
using UnityEngine;

namespace TestAssignment.Characters.Interfaces
{
    public interface IShooting
    {
        float ShootingSpeed { get; }
        float Damage { get; }
        Weapon Weapon { get; }
        public BaseCharacterComponent Target { get; }
        public Transform Transform { get; }
        float RotationSpeed { get; }

        void SetTarget(BaseCharacterComponent target);
    }
}