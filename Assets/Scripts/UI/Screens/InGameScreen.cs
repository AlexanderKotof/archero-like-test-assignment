using TestAssignment.Characters;
using TestAssignment.Core.Data;
using TestAssignment.UI.Components;
using UnityEngine.UI;

namespace TestAssignment.UI.Screens
{
    public class InGameScreen : BaseScreen
    {
        public TMPro.TMP_Text healthCountText;
        public TMPro.TMP_Text levelNumberText;
        public TMPro.TMP_Text coinsCountText;

        public Button pauseButton;
        public JoystickComponent joystick;

        private PlayerComponent _player;
        private PlayerData _data;

        public void BindPlayer(PlayerComponent player, PlayerData data)
        {
            _player = player;
            _data = data;
        }

        private void Update()
        {
            if (_player == null || _data == null)
                return;

            healthCountText.SetText($"Health - {_player.CurrentHealth} / {_player.StartHealth}");
            levelNumberText.SetText($"Level {_data.CurrentLevel}");
            coinsCountText.SetText($"${_data.PlayerCoins}");
        }
    }
}
