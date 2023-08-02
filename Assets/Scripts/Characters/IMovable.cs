using UnityEngine;

namespace TestAssignment.Characters.Interfaces
{
    public interface IMovable
    {
        float MoveSpeed { get; }

        Vector3 MovementDirection { get; }

        Rigidbody Rigidbody { get; }

        void Move(Vector3 direction);
    }
}