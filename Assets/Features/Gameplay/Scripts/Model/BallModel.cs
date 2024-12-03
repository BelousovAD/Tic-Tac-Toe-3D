namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Шар
    /// </summary>
    public class BallModel
    {
        /// <summary>
        /// Тип шара
        /// </summary>
        public BallType BallType => _ballType;
        private BallType _ballType = BallType.Black;

        /// <summary>
        /// Позиция шара в кубе
        /// </summary>
        public Vector3Int Position => _position;
        private Vector3Int _position = Vector3Int.zero;

        public BallModel(BallType ballType, Vector3Int position)
        {
            _ballType = ballType;
            _position = position;
        }
    }
}
