namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
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

        /// <summary>
        /// Тип шара изменён
        /// </summary>
        public event Action<BallType> onBallTypeChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Тип шара
        /// </summary>
        public BallType Type
        {
            get => _type;
            set
            {
                if (_type == BallType.None && _type != value)
                {
                    _type = value;
                    onBallTypeChanged(_type);
                }
            }
        }
        private BallType _type = BallType.None;

        /// <summary>
        /// Позиция шара в кубе
        /// </summary>
        public Vector3Int Position => _position;
        private Vector3Int _position = Vector3Int.zero;

        /// <summary>
        /// Приоритет позиции шара
        /// </summary>
        public int Priority
        {
            get
            {
                int sum = 0;
                foreach (Line line in LinkedLines)
                {
                    sum += line.Priority;
                }
                return sum;
            }
        }

        /// <summary>
        /// Линии, к которым принадлежит шар
        /// </summary>
        public List<Line> LinkedLines => _linkedLines;
        private List<Line> _linkedLines = new();

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

        public Ball() { }

        public Ball(Vector3Int position, BallType ballType)
        {
            _type = ballType;
            _position = position;
        }

        #endregion
    }
}
