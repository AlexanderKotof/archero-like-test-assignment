using TestAssignment.Characters;
using TestAssignment.Level;
using UnityEngine;

namespace TestAssignment.Core.Settings
{
    [CreateAssetMenu(menuName = "Settings/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        public int PreStartDelay => _preStartDelay;
        [SerializeField]
        private int _preStartDelay;

        public PlayerComponent PlayerPrefab => _playerPrefab;
        [SerializeField]
        private PlayerComponent _playerPrefab;

        public CharacterComponent[] EnemiesPrefabs => _enemiesPrefabs;
        [SerializeField]
        private CharacterComponent[] _enemiesPrefabs;

        public int MinEnemiesCount => _minEnemiesCount;
        [SerializeField]
        private int _minEnemiesCount;
        public int MaxEnemiesCount => _maxEnemiesCount;
        [SerializeField]
        private int _maxEnemiesCount;

        public GameFieldComponent LevelFieldPrefab => _levelFieldPrefab;
        [SerializeField]
        private GameFieldComponent _levelFieldPrefab;


        public GatesComponent GatesPrefab => _gatesPrefab;
        [SerializeField]
        private GatesComponent _gatesPrefab;

        public LevelObstacle[] PossibleObstacles => _possibleObstacles;
        [SerializeField]
        private LevelObstacle[] _possibleObstacles;

        public int MinObstaclesCount => _minObstaclesCount;
        public int MaxObstaclesCount => _maxObstaclesCount;
        [SerializeField]
        private int _minObstaclesCount;
        [SerializeField]
        private int _maxObstaclesCount;

        public int LevelSizeX => _levelSizeX;
        [SerializeField]
        private int _levelSizeX;
        public int LevelSizeY => _levelSizeY;
        [SerializeField]
        private int _levelSizeY;
    }
}