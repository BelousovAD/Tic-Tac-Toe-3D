namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Спавнер шаров
    /// </summary>
    public class BallSpawner : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Данные шара, который будет спавнится
        /// </summary>
        public BallData BallData => ballData;
        [SerializeField]
        protected BallData ballData = default;

        [SerializeField]
        protected Transform parent = default;

        protected Cube cube = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(Cube _cube)
            => cube = _cube;

        /// <summary>
        /// Заспавнить шар
        /// </summary>
        /// <param name="ballSpawnPosition">Позиция спавна шара</param>
        public virtual void SpawnBall(BallSpawnPosition ballSpawnPosition)
        {
            Ball ball = cube.GetBallAt(ballSpawnPosition.NextBallPosition);
            BallView ballView = Instantiate(ballData.Prefab, ballSpawnPosition.transform.position, UnityEngine.Random.rotation, parent);
            ballView.Ball = ball;
            cube.TryAddBall(ballSpawnPosition.NextBallPosition, ballData.Type);
        }

        #endregion
    }
}
