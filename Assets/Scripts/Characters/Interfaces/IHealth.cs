namespace TestAssignment.Characters.Interfaces
{
    public interface IHealth
    {
        float StartHealth { get; }
        float CurrentHealth { get; }

        bool IsDied { get; }

        delegate void OnDied(BaseCharacterComponent character);

        void TakeDamage(float damage);

        void RestoreHealth(float value);
    }
}