using TestAssignment.Characters;
using TestAssignment.Level;
using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Game Settings")]
public class GameSettings : ScriptableObject
{
    public PlayerComponent playerPrefab;

    public CharacterComponent[] enemiesPrefabs;

    public int minEnemiesCount;
    public int maxEnemiesCount;

    public GameFieldComponent levelFieldPrefab;

    public GatesComponent gatesPrefab;

    public LevelObstacle[] possibleObstacles;

    public int minObstaclesCount;
    public int maxObstaclesCount;

    public int levelSizeX;
    public int levelSizeY;
}
