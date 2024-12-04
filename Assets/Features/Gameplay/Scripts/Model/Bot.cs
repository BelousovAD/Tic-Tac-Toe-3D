namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Бот
    /// </summary>
    public class Bot : Player
    {
        #region Properties

        private BallSpawnPosition[,] m_ballSpawnPositions;

        #endregion

        #region Methods

        public Bot(BallSpawnPosition[] ballSpawnPositions)
            : base("Bot")
        {
            int dimension = (int)Mathf.Sqrt(ballSpawnPositions.Length);
            m_ballSpawnPositions = new BallSpawnPosition[dimension, dimension];

            foreach (BallSpawnPosition ballSpawnPosition in ballSpawnPositions)
            {
                Vector3Int position = ballSpawnPosition.BallPosition;
                m_ballSpawnPositions[position.x, position.y] = ballSpawnPosition;
            }
        }

        #endregion
    }
}
