namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Шар
    /// </summary>
    public class Ball
    {
        #region Events

        /// <summary>
        /// Изменена подсветка шара
        /// </summary>
        public event Action onHighlightChanged = delegate { };

        #endregion

        #region Properties

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

        /// <summary>
        /// Подсвечен ли шар
        /// </summary>
        public bool IsHighlighted
        {
            get => _isHighlighted;
            set
            {
                if (value != _isHighlighted)
                {
                    _isHighlighted = value;
                    onHighlightChanged();
                }
            }
        }
        private bool _isHighlighted = false;

        #endregion

        #region Methods

        public Ball(BallType ballType, Vector3Int position)
        {
            _ballType = ballType;
            _position = position;
        }

        #endregion
    }
}
