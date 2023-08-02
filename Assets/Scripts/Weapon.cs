using UnityEngine;

namespace TestAssignment.Weapons
{
    [CreateAssetMenu(menuName = "Settings/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private string _description;
        [SerializeField]
        private int _damage;
        [SerializeField]
        private float _shootingSpeed;
        [SerializeField]
        private Projectile _projectile;

        public string Name => _name;
        public string Description => _description;
        public int Damage => _damage;
        public float ShootingSpeed => _shootingSpeed;
        public Projectile Projectile => _projectile;
    }
}