namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Абстрактный контейнер шаров для определения победителя
    /// </summary>
    public abstract class AbstractBallsContainer
    {
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

        #endregion

        #region Methods

        /// <summary>
        /// Пытается добавить шар в контейнер
        /// </summary>
        /// <param name="ball">Добавляемый шар</param>
        public abstract void TryAddBall(BallModel ball);

        /// <summary>
        /// Очистить контейнер от шаров и сбросить свойства
        /// </summary>
        public abstract void Reset();

        protected virtual void UpdateFilledStatus(AbstractBallsContainer ballsContainer)
        {
            if (!ballsContainer.IsFull)
            {
                IsFull = false;
            }
        }

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
