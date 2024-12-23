namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Спавнер шаров
    /// </summary>
    public class BallSpawner : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Шар заспавнен
        /// </summary>
        public static event Action<Ball> onBallSpawned = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Данные шара, который будет спавнится
        /// </summary>
        public BallData BallData => ballData;
        [SerializeField]
        protected BallData ballData = default;

        [SerializeField]
        protected Transform parent = default;

        #endregion

        #region Methods

        protected virtual void OnEnable()
            => BallSpawnPositionController.onBallSpawn += SpawnBall;

        protected virtual void OnDisable()
            => BallSpawnPositionController.onBallSpawn -= SpawnBall;

        protected virtual void SpawnBall(BallSpawnPosition ballSpawnPosition)
        {
            Ball ball = new(ballData.Type, ballSpawnPosition.NextBallPosition);
            BallView ballView = Instantiate(ballData.Prefab, ballSpawnPosition.transform.position, UnityEngine.Random.rotation, parent);
            ballView.Ball = ball;
            onBallSpawned(ball);
        }

        #endregion
    }
}
