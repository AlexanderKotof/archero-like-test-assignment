namespace TestAssignment.Characters.Interfaces
{
    public interface IShooting
    {
        float ShootingSpeed { get; }
        float Damage { get; }
        Weapon Weapon { get; }
        public CharacterComponent Target { get; }
    }
}