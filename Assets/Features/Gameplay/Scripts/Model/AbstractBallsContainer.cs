using System;
using UnityEngine;

namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Абстрактный контейнер шаров для определения победителя
    /// </summary>
    public abstract class AbstractBallsContainer : IDisposable
    {
        #region Events

        /// <summary>
        /// Версия данных контейнера изменилась
        /// </summary>
        public event Action onVersionChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Заполнен ли контейнер
        /// </summary>
        public virtual bool IsFull
        {
            get => isFull;
            protected set
            {
                if (value != isFull)
                {
                    isFull = value;
                }
            }
        }
        protected bool isFull = false;

        /// <summary>
        /// Победитель
        /// </summary>
        public virtual BallType Winner
        {
            get => winner;
            protected set
            {
                if (value != winner)
                {
                    winner = value;
                }
            }
        }
        protected BallType winner = BallType.None;

        /// <summary>
        /// Версия данных контейнера
        /// </summary>
        public int Version
        {
            get => version;
            protected set
            {
                if (value > version)
                {
                    version = value;
                    onVersionChanged();
                }
            }
        }
        protected int version = 0;

        #endregion

        #region Methods

        public abstract void Dispose();

        /// <summary>
        /// Добавить модель шара в контейнер
        /// </summary>
        /// <param name="ball">Добавляемый шар</param>
        public abstract void AddBallModel(Ball ball);

        /// <summary>
        /// Получить модель шара из контейнера
        /// </summary>
        /// <param name="position">Позиция шара в контейнере</param>
        /// <returns>Модель шара</returns>
        public abstract Ball GetBallAt(Vector3Int position);

        /// <summary>
        /// Попытка добавить шар в контейнер
        /// </summary>
        /// <param name="position">Позиция шара в контейнере</param>
        /// <param name="ballType">Тип шара</param>
        /// <returns>Добавлен ли шар в контейнер</returns>
        public abstract bool TryAddBall(Vector3Int position, BallType ballType);

        protected virtual void UpdateFilledStatus(AbstractBallsContainer ballsContainer)
            => IsFull = IsFull && ballsContainer.IsFull;

        protected virtual void UpdateWinner(AbstractBallsContainer ballsContainer)
        {
            if (ballsContainer.Winner != BallType.None)
            {
                Winner = ballsContainer.Winner;
            }
        }

        #endregion
    }
}
