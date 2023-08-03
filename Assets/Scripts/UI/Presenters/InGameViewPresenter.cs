using System;
using TestAssignment.Characters;
using TestAssignment.Core.Data;
using TestAssignment.Input;
using TestAssignment.UI.Screens;

namespace TestAssignment.UI.Presenters
{
    public static class InGameViewPresenter
    {
        public static void ShowInGameScreen(PlayerComponent player, PlayerData data, InputManager input, Action onPausePressed)
        {
            var inGameScreen = UIManager.ShowOnly<InGameScreen>();
            inGameScreen.joystick.OnJoystickInput += input.SetMovementInput;
            inGameScreen.pauseButton.onClick.AddListener(onPausePressed.Invoke);
            inGameScreen.BindPlayer(player, data);
        }
    }
}
