using Newbeedev.ObjectsPool;
using System.Collections.Generic;
using UnityEngine;

namespace TestAssignment.Level.Generator
{
    public class LevelGenerator
    {
        private GameSettings _gameSettings;

        public LevelGenerator(GameSettings settings)
        {
            _gameSettings = settings;
        }

        public LevelData Generate()
        {
            var levelData = new LevelData();

            var levelField = ObjectSpawnManager.Spawn(_gameSettings.levelFieldPrefab);
            levelField.SetSize(_gameSettings.levelSizeX, _gameSettings.levelSizeY);

            var playerCell = new Vector3(0, 0, -_gameSettings.levelSizeY / 2 + 1);
            levelData.player = ObjectSpawnManager.Spawn(_gameSettings.playerPrefab, playerCell, Quaternion.identity);

            var gatesCell = new Vector3(0, 0, _gameSettings.levelSizeY / 2);
            levelData.gates = ObjectSpawnManager.Spawn(_gameSettings.gatesPrefab, gatesCell, Quaternion.identity);

            List<Vector3> occupiedCels = new List<Vector3>(GetPreoccupiedCels(playerCell, gatesCell));

            GenerateObstacles(occupiedCels);
            GenerateEnemies(occupiedCels);

            return levelData;

            void GenerateObstacles(List<Vector3> occupiedCells)
            {
                var obstaclesCount = Random.Range(_gameSettings.minObstaclesCount, _gameSettings.maxObstaclesCount);
                for (int i = 0; i < obstaclesCount; i++)
                {
                    var randomObstacle = _gameSettings.possibleObstacles[Random.Range(0, _gameSettings.possibleObstacles.Length)];
                    Vector3 randomPosition = GenerateRandomUnoccupiedPositionInBounds(occupiedCells, _gameSettings.levelSizeX / 2, _gameSettings.levelSizeY / 2);
                    ObjectSpawnManager.Spawn(randomObstacle, randomPosition, Quaternion.identity);
                    occupiedCells.Add(randomPosition);
                }
            }
            void GenerateEnemies(List<Vector3> occupiedCells)
            {
                var enemiesCount = Random.Range(_gameSettings.minEnemiesCount, _gameSettings.maxEnemiesCount);
                for (int i = 0; i < enemiesCount; i++)
                {
                    var randomEnemy = _gameSettings.enemiesPrefabs[Random.Range(0, _gameSettings.enemiesPrefabs.Length)];
                    Vector3 randomPosition = GenerateRandomUnoccupiedPositionInBounds(occupiedCells, _gameSettings.levelSizeX / 2, _gameSettings.levelSizeY / 3);
                    var enemy = ObjectSpawnManager.Spawn(randomEnemy, randomPosition, Quaternion.identity);
                    levelData.spawnedEnemies.Add(enemy);
                    occupiedCells.Add(randomPosition);
                }
            }
        }

        // exclude cells arround player and gates
        private Vector3[] GetPreoccupiedCels(Vector3 playerCell, Vector3 gatesCell)
        {
            return new Vector3[]
            {
                playerCell,
                playerCell + Vector3.forward,
                playerCell + Vector3.left,
                playerCell + Vector3.right,
                playerCell + Vector3.right + Vector3.forward,
                playerCell + Vector3.left + Vector3.forward,
                gatesCell,
                gatesCell + Vector3.right,
                gatesCell + Vector3.back,
                gatesCell + Vector3.left,
                gatesCell + Vector3.right + Vector3.back,
                gatesCell + Vector3.left + Vector3.back,
            };
        }

        private Vector3 GenerateRandomUnoccupiedPositionInBounds(List<Vector3> occupiedCels, int xBound, int yBounds)
        {
            bool isOccupied;
            Vector3 randomPosition;

            do
            {
                isOccupied = false;
                randomPosition = new Vector3(
                    Random.Range(-xBound, xBound),
                    0,
                    Random.Range(-yBounds, yBounds)
                    );

                foreach (var cell in occupiedCels)
                {
                    if (randomPosition != cell)
                        continue;

                    isOccupied = true;
                    break;
                }
            } while (isOccupied);
            return randomPosition;
        }
    }
}