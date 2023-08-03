using System;
using TestAssignment.UI.Screens;

namespace TestAssignment.UI.Presenters
{
    public static class PauseViewPresenter
    {
        public static void ShowPauseScreen(Action onResumePressed)
        {
            var pauseScreen = UIManager.ShowOnly<PauseScreen>();
            pauseScreen.resumeButton.onClick.AddListener(onResumePressed.Invoke);
        }
    }
}
