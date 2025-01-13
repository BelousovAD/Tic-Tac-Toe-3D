namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Контроллер спавнеров шаров
    /// </summary>
    public class BallSpawnersController : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Спавнеры шаров
        /// </summary>
        public List<BallSpawner> BallSpawners => ballSpawners;
        [SerializeField]
        protected List<BallSpawner> ballSpawners = new();

        protected IEnumerator<BallSpawner> ballSpawnersEnumerator = default;
        protected TurnController turnController = default;

        #endregion

        #region Methods

        protected virtual void Start()
        {
            ballSpawnersEnumerator = ballSpawners.GetEnumerator();
            MoveToNextBallSpawner();
        }

        [Inject]
        protected virtual void Construct(TurnController _turnController)
        {
            turnController = _turnController;
            turnController.onTurnPrepare += MoveToNextBallSpawner;
            BallSpawnPositionController.onBallSpawn += SpawnBall;
        }

        protected virtual void OnDestroy()
        {
            turnController.onTurnPrepare -= MoveToNextBallSpawner;
            BallSpawnPositionController.onBallSpawn -= SpawnBall;
        }

        protected virtual void SpawnBall(BallSpawnPosition ballSpawnPosition)
            => ballSpawnersEnumerator.Current.SpawnBall(ballSpawnPosition);

        protected virtual void MoveToNextBallSpawner()
        {
            if (!ballSpawnersEnumerator.MoveNext())
            {
                ballSpawnersEnumerator.Reset();
                ballSpawnersEnumerator.MoveNext();
            }
        }

        #endregion
    }
}
