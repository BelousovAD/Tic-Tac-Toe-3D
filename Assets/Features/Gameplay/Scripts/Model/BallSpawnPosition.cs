namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Позиция спавна шара
    /// </summary>
    public class BallSpawnPosition : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Видимость позиции изменилась
        /// </summary>
        public event Action onVisibleChanged = delegate { };

        #endregion

        #region Properties

        [SerializeField]
        protected Vector2Int position = Vector2Int.zero;

        /// <summary>
        /// Следующая позиция шара в кубе
        /// </summary>
        public Vector3Int NextBallPosition => _nextBallPosition;
        private Vector3Int _nextBallPosition = Vector3Int.zero;

        /// <summary>
        /// Является ли позиция видимой
        /// </summary>
        public bool IsVisible
        {
            get => isVisible;
            protected set
            {
                if (value != isVisible)
                {
                    isVisible = value;
                    onVisibleChanged();
                }
            }
        }
        protected bool isVisible = true;

        [Inject]
        protected GameSettings gameSettings = default;

        #endregion

        #region Methods

        /// <summary>
        /// Увеличить следующую позицию шара
        /// </summary>
        public virtual void IncreaseNextPosition()
        {
            if (NextBallPosition.z < gameSettings.Rank)
            {
                ++_nextBallPosition.z;
            }
            else
            {
                IsVisible = false;
            }
        }

        /// <summary>
        /// Сбросить значения свойств
        /// </summary>
        public virtual void ResetToDefault()
        {
            _nextBallPosition.z = 0;
            IsVisible = true;
        }

        protected virtual void Awake() => _nextBallPosition = new(position.x, position.y, 0);

        #endregion
    }
}
