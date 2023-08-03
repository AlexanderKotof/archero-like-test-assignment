using System;
using UnityEngine;

namespace TestAssignment.Input
{
    public class InputManager
    {
        public static event Action<Vector2> MovementInput;

        public void SetMovementInput(Vector2 input)
        {
            MovementInput?.Invoke(input);
        }
    }
}