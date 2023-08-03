using Newbeedev.ObjectsPool;
using System.Collections.Generic;
using TestAssignment.Characters;
using TestAssignment.Level.Generator;
using UnityEngine;

namespace TestAssignment.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public PlayerComponent Player { get; private set; }

        [SerializeField]
        private GameSettings _gameSettings;

        private LevelGenerator _levelGenerator;

        private List<CharacterComponent> _spawnedEnemies = new List<CharacterComponent>();

        public bool GameStarted { get; private set; }

        public event System.Action LevelCompleted;

        private void Awake()
        {
            Instance = this;
            _levelGenerator = new LevelGenerator(_gameSettings);
        }

        private void Start()
        {
            GameBegins();
        }

        private void GameBegins()
        {
            GameStarted = false;

            CreateLevel();

            Invoke(nameof(StartGame), 3f);
        }

        private void CreateLevel()
        {
            var data = _levelGenerator.Generate();
            Player = data.player;
            Player.CharacterDied = OnPlayerDied;

            _spawnedEnemies = data.spawnedEnemies;
            foreach (var enemy in _spawnedEnemies)
            {
                enemy.CharacterDied = () => OnEnemyDied(enemy);
                enemy.SetTarget(Player);
            }

            data.gates.GoToNextLevel = RegenerateLevel;
        }

        private void StartGame()
        {
            GameStarted = true;
        }

        private void RegenerateLevel()
        {
            ObjectSpawnManager.DespawnAll();
            _spawnedEnemies.Clear();
            GameBegins();
        }

        private void OnPlayerDied()
        {
            RegenerateLevel();
            Player.RestoreHealth();
        }

        private void OnEnemyDied(CharacterComponent enemy)
        {
            ObjectSpawnManager.Despawn(enemy);

            _spawnedEnemies.Remove(enemy);

            if (_spawnedEnemies.Count == 0)
                LevelCompleted?.Invoke();
        }

        public CharacterComponent GetNearestEnemyToPlayer()
        {
            const float _distanceThreashold = 1f;

            var distance = float.MaxValue;
            CharacterComponent nearest = null;

            foreach (var enemy in _spawnedEnemies)
            {
                if (!ShootingUtils.TargetIsVisible(Player, enemy))
                    continue;

                var distanceToEnemy = (enemy.transform.position - Player.transform.position).sqrMagnitude;
                if (distanceToEnemy + _distanceThreashold < distance)
                {
                    nearest = enemy;
                    distance = distanceToEnemy;
                }
            }
            return nearest;
        }
    }
}