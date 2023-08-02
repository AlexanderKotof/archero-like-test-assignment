using System.Collections.Generic;
using TestAssignment.Characters;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    private GameSettings _gameSettings;
    public List<CharacterComponent> SpawnedEnemies { get; private set; } = new List<CharacterComponent>();
    public PlayerComponent Player { get; private set; }

    public Transform playerSpawnPoint;

    public static bool GameStarted;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        CreateLevel();
        Invoke(nameof(StartGame), 3f);
    }

    private void CreateLevel()
    {
        var levelField = Instantiate(_gameSettings.levelFieldPrefab);
        levelField.SetSize(_gameSettings.levelSizeX, _gameSettings.levelSizeY);

        var playerCell = new Vector3(0, 0, -_gameSettings.levelSizeY / 2 + 1);
        Player = Instantiate(_gameSettings.playerPrefab, playerCell, Quaternion.identity);

        List<Vector3> occupiedCels = new List<Vector3>() { playerCell };

        GenerateObstacles();
        GenerateEnemies();

        void GenerateObstacles()
        {
            var obstaclesCount = Random.Range(_gameSettings.minObstaclesCount, _gameSettings.maxObstaclesCount);
            for (int i = 0; i < obstaclesCount; i++)
            {
                var randomObstacle = _gameSettings.possibleObstacles[Random.Range(0, _gameSettings.possibleObstacles.Length)];
                Vector3 randomPosition = GenerateRandomUnoccupiedPosition(occupiedCels);
                Instantiate(randomObstacle, randomPosition, Quaternion.identity);
                occupiedCels.Add(randomPosition);
            }
        }
        void GenerateEnemies()
        {
            var enemiesCount = Random.Range(_gameSettings.minEnemiesCount, _gameSettings.maxEnemiesCount);
            for (int i = 0; i < enemiesCount; i++)
            {
                var randomEnemy = _gameSettings.enemiesPrefabs[Random.Range(0, _gameSettings.enemiesPrefabs.Length)];
                Vector3 randomPosition = GenerateRandomUnoccupiedPosition(occupiedCels);
                Instantiate(randomEnemy, randomPosition, Quaternion.identity);
                occupiedCels.Add(randomPosition);
            }
        }
    }

    private Vector3 GenerateRandomUnoccupiedPosition(List<Vector3> occupiedCels)
    {
        bool isOccupied;
        Vector3 randomPosition;

        do
        {
            isOccupied = false;
            randomPosition = new Vector3(
                Random.Range(-_gameSettings.levelSizeX / 2, _gameSettings.levelSizeX / 2),
                0,
                Random.Range(-_gameSettings.levelSizeY / 3, _gameSettings.levelSizeY / 2));

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

    void StartGame()
    {
        GameStarted = true;
    }

    void Update()
    {
        
    }
}