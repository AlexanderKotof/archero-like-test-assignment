using System;
using TestAssignment.Core.Data;
using TestAssignment.UI.Screens;

namespace TestAssignment.UI.Presenters
{
    public static class DefeatedViewPresenter
    {
        public static void ShowDefeatedScreen(PlayerData data, Action onRestartPressed)
        {
            var defeatedScreen = UIManager.ShowOnly<DefeatedScreen>();
            defeatedScreen.restartButton.onClick.AddListener(onRestartPressed.Invoke);
            defeatedScreen.SetInfo(data);
        }
    }
}
