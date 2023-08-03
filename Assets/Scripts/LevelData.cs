using System.Collections.Generic;
using TestAssignment.Characters;

namespace TestAssignment.Level.Generator
{
    public class LevelData
    {
        public PlayerComponent player;
        public GatesComponent gates;
        public List<CharacterComponent> spawnedEnemies = new List<CharacterComponent>();
    }
}