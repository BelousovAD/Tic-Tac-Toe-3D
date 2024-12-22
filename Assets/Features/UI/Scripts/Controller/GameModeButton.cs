namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Gameplay;
    using UnityEngine;

    /// <summary>
    /// Кнопка выбора режима игры
    /// </summary>
    public class GameModeButton : AbstractGameSettingsButton
    {
        #region Properties

        [SerializeField]
        protected GameMode gameMode = GameMode.WithBot;

        #endregion

        #region Methods

        public override void OnClick()
            => gameSettings.GameMode = gameMode;

        #endregion
    }
}
