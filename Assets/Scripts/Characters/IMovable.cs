using UnityEngine;

namespace TestAssignment.Characters.Interfaces
{
    public interface IMovable
    {
        float MoveSpeed { get; }

        Vector3 MovementDirection { get; set; }

        Rigidbody Rigidbody { get; }
    }

    public interface IDistanceMovable : IMovable
    {
        float MovingDistance { get; }
    }
}