using Newbeedev.ObjectsPool;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestAssignment.Characters;
using TestAssignment.Characters.Interfaces;
using TestAssignment.Core.Data;
using TestAssignment.Core.Settings;
using TestAssignment.Input;
using TestAssignment.Level.Generator;
using TestAssignment.UI.Presenters;
using UnityEngine;

namespace TestAssignment.Core
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public PlayerComponent Player { get; private set; }
        public List<BaseCharacterComponent> SpawnedEnemies { get; private set; } = new List<BaseCharacterComponent>();
        public bool GameStarted { get; private set; }
        public PlayerData PlayerData { get; private set; }

        [SerializeField]
        private GameSettings _gameSettings;
        private LevelGenerator _levelGenerator;
        private InputManager _input;

        public event Action LevelCompleted;

        private void Awake()
        {
            Instance = this;
            _levelGenerator = new LevelGenerator(_gameSettings);
            _input = new InputManager();
            PlayerData = new PlayerData()
            {
                CurrentLevel = 1,
                PlayerCoins = 0,
            };
        }
        private void Start()
        {
            GameStarted = false;
            CreateLevel();
            MainMenuViewPresenter.ShowMainMenu(StartCountdown);
        }

        private void StartNewGame()
        {
            GameStarted = false;
            CreateLevel();
            StartCountdown();
        }
        private void GoToNextLevel()
        {
            PlayerData.CurrentLevel++;
            ClearLevel();
            StartNewGame();
        }
        private async void StartCountdown()
        {
            PreStartViewPresenter.ShowPreStartScreen();

            var delay = _gameSettings.PreStartDelay;
            for (int i = 0; i < delay; i++)
            {
                PreStartViewPresenter.UpdateCounter(delay - i);
                await Task.Delay(1000);
            }

            InGameViewPresenter.ShowInGameScreen(Player, PlayerData, _input, OnPausePressed);
            GameStarted = true;
        }

        private void CreateLevel()
        {
            var data = _levelGenerator.Generate();
            Player = data.player;
            Player.CharacterDied = OnPlayerDied;

            SpawnedEnemies = data.spawnedEnemies;
            foreach (var enemy in SpawnedEnemies)
            {
                enemy.CharacterDied = () => OnEnemyDied(enemy);
                enemy.SetTarget(Player);
            }

            data.gates.GoToNextLevel = GoToNextLevel;
        }
        private void ClearLevel()
        {
            ObjectSpawnManager.DespawnAll();
            SpawnedEnemies.Clear();
        }

        private void OnPausePressed()
        {
            PauseViewPresenter.ShowPauseScreen(OnResumePressed);
            Time.timeScale = 0;
        }
        private void OnResumePressed()
        {
            InGameViewPresenter.ShowInGameScreen(Player, PlayerData, _input, OnPausePressed);
            Time.timeScale = 1;
        }

        private void OnPlayerDied()
        {
            DefeatedViewPresenter.ShowDefeatedScreen(PlayerData, RestartGame);
            ObjectSpawnManager.Despawn(Player);

            void RestartGame()
            {
                Player.RestoreHealth();
                PlayerData.PlayerCoins = 0;
                PlayerData.CurrentLevel = 1;

                ClearLevel();
                StartNewGame();
            }
        }
        private void OnEnemyDied(BaseCharacterComponent enemy)
        {
            ObjectSpawnManager.Despawn(enemy);

            SpawnedEnemies.Remove(enemy);

            if (enemy is IRewardable rewardableEnemy)
                PlayerData.PlayerCoins += rewardableEnemy.GetReward();

            if (SpawnedEnemies.Count == 0)
                LevelCompleted?.Invoke();
        }
    }
}