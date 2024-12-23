namespace TicTacToe3D.Features.DependencyInjection
{
    using TicTacToe3D.Features.Gameplay;
    using Zenject;

    /// <summary>
    /// Установщик игры
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        /// <summary>
        /// Устанавливает привязки
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<Cube>()
                .AsSingle()
                .CopyIntoAllSubContainers()
                .NonLazy();
            Container.Bind<BallsContainerController<Cube>>()
                .AsSingle()
                .CopyIntoAllSubContainers()
                .NonLazy();
            Container.Bind<GameStateController>()
                .AsSingle()
                .CopyIntoAllSubContainers()
                .NonLazy();
        }
    }
}
