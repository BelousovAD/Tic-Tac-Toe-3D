using System;
using Zenject;

namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Контроллер контейнера шаров
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BallsContainerController<T> : IDisposable where T : AbstractBallsContainer
    {
        #region Properties

        /// <summary>
        /// Контейнер шаров
        /// </summary>
        public T BallsContainer => ballsContainer;
        protected T ballsContainer = default;

        #endregion

        #region Methods

        [Inject]
        public BallsContainerController(T container)
        {
            ballsContainer = container;
            BallSpawner.onBallSpawned += AddBall;
        }

        public void Dispose()
            => BallSpawner.onBallSpawned -= AddBall;

        protected virtual void AddBall(Ball ball)
            => ballsContainer.TryAddBall(ball);

        #endregion
    }
}
