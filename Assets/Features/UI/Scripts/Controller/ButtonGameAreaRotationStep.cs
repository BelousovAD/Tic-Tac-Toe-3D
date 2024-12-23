namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using TicTacToe3D.Features.Gameplay;
    using Zenject;

    /// <summary>
    /// Кнопка переключения шага вращения игрового поля
    /// </summary>
    public class ButtonGameAreaRotationStep : AbstractButton
    {
        #region Properties

        protected GameAreaRotator gameAreaRotator = default;

        #endregion

        #region Methods

        public override void OnClick()
            => gameAreaRotator.MoveToNextStep();

        [Inject]
        protected virtual void Construct(GameAreaRotator _gameAreaRotator)
            => gameAreaRotator = _gameAreaRotator;

        #endregion
    }
}
