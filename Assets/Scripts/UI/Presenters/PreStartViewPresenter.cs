using TestAssignment.UI.Screens;

namespace TestAssignment.UI.Presenters
{
    public static class PreStartViewPresenter
    {
        public static void ShowPreStartScreen()
        {
            UIManager.ShowOnly<PreStartScreen>();
        }

        public static void UpdateCounter(int value)
        {
            UIManager.GetScreen<PreStartScreen>().preStartCounterText.SetText(value.ToString());
        }
    }
}
