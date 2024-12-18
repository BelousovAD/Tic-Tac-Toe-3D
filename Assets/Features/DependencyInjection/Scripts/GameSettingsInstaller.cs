namespace TicTacToe3D.Features.DependencyInjection
{
    using TicTacToe3D.Features.Gameplay;
    using Zenject;

    /// <summary>
    /// Установщик настроек игры
    /// </summary>
    public class GameSettingsInstaller : MonoInstaller
    {
        /// <summary>
        /// Устанавливает привязки
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<GameSettings>()
                .AsSingle()
                .CopyIntoAllSubContainers()
                .NonLazy();
        }
    }
}
