namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Gameplay;
    using UnityEngine;

    /// <summary>
    /// Кнопка выбора типа шара
    /// </summary>
    public class BallTypeButton : AbstractGameSettingsButton
    {
        #region Properties

        [SerializeField]
        protected BallType ballType = BallType.White;

        #endregion

        #region Methods

        public override void OnClick()
            => gameSettings.BallType = ballType;

        #endregion
    }
}
