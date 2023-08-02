using System;
using System.Collections;
using System.Collections.Generic;
using TestAssignment.Characters;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<CharacterComponent> _spawnedEnemies = new List<CharacterComponent>();
    public PlayerComponent player;

    public Transform playerSpawnPoint;


    public static bool GameStarted;
    void Start()
    {
        CreateLevel();
        Invoke(nameof(StartGame), 3f);
    }

    private void CreateLevel()
    {
        
    }

    void StartGame()
    {
        GameStarted = true;
    }

    void Update()
    {
        
    }
}
