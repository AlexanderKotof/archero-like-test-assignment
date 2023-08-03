using System;
using TestAssignment.UI.Screens;

namespace TestAssignment.UI.Presenters
{
    public static class MainMenuViewPresenter
    {
        public static void ShowMainMenu(Action startButtonCallback)
        {
            var startGameScreen = UIManager.ShowOnly<MenuScreen>();
            startGameScreen.startGameButton.onClick.AddListener(startButtonCallback.Invoke);
        }
    }
}
