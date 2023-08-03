using Newbeedev.ObjectsPool;
using UnityEngine;

namespace TestAssignment.Characters.Drop
{
    public class CharacterDropComponent : MonoBehaviour
    {
        [SerializeField]
        private BaseDropItemComponent[] _possibleDrop;

        [SerializeField, Range(0, 1)]
        private float _dropChance;

        private BaseCharacterComponent _character;

        private void Awake()
        {
            _character = GetComponent<BaseCharacterComponent>();
        }

        private void OnEnable()
        {
            _character.CharacterDied += CharacterDied;
        }

        private void OnDisable()
        {
            _character.CharacterDied -= CharacterDied;
        }

        private void CharacterDied(BaseCharacterComponent character)
        {
            var random = Random.value;

            if (random > _dropChance)
                return;

            var drop = _possibleDrop[Random.Range(0, _possibleDrop.Length)];
            ObjectSpawnManager.Spawn(drop, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
