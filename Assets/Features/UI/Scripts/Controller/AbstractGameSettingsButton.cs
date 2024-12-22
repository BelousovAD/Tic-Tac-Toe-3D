namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using TicTacToe3D.Features.Gameplay;
    using Zenject;

    /// <summary>
    /// Абстрактная кнопка для настроек игры
    /// </summary>
    public abstract class AbstractGameSettingsButton : AbstractButton
    {
        #region Properties

        protected GameSettings gameSettings = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(GameSettings settings)
            => gameSettings = settings;

        #endregion
    }
}
