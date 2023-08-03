using Newbeedev.ObjectsPool;
using UnityEngine;

namespace TestAssignment.Characters.Drop
{
    public class DropHealthComponent : BaseDropItemComponent
    {
        [SerializeField]
        private float _restoreHealth;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerComponent>(out var character))
            {
                character.RestoreHealth(_restoreHealth);
                ObjectSpawnManager.Despawn(this);
            }
        }
    }
}