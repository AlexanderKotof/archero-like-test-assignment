using System;
using UnityEngine;

namespace TestAssignment.Input
{
    public class InputManager : IDisposable
    {
        public static event Action<Vector2> MovementInput;

        public void Dispose()
        {
            MovementInput = null;
        }

        public void SetMovementInput(Vector2 input)
        {
            MovementInput?.Invoke(input);
        }
    }
}