using Newbeedev.ObjectsPool;
using TestAssignment.Weapons;
using UnityEngine;

namespace TestAssignment.Characters.Drop
{
    public class DropWeaponComponent : BaseDropItemComponent
    {
        [SerializeField]
        private Weapon _weapon;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerComponent>(out var character))
            {
                character.SetWeapon(_weapon);
                ObjectSpawnManager.Despawn(this);
            }
        }
    }
}