namespace TestAssignment.Characters.Interfaces
{
    public interface IHealth
    {
        float StartHealth { get; }
        float CurrentHealth { get; }

        bool IsDied { get; }

        delegate void OnDied();

        void TakeDamage(float damage);

        void RestoreHealth();
    }
}