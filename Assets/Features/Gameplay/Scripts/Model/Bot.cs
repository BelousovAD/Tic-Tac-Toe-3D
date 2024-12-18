namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Бот
    /// </summary>
    public class Bot : Player
    {
        #region Properties

        public override TurnController TurnController
        {
            protected get => base.TurnController;
            set
            {
                if (value != TurnController)
                {
                    if (TurnController != null)
                    {
                        TurnController.onTurnChanged -= MakeMove;
                    }

                    base.TurnController = value;

                    if (TurnController != null)
                    {
                        TurnController.onTurnChanged += MakeMove;
                    }
                }
            }
        }

        private BallSpawnPositionController[,] m_ballSpawnPositions;

        #endregion

        #region Methods

        public Bot(BallSpawner ballSpawner, string name = "Bot")
            : base(ballSpawner, name)
        { }

        protected virtual void MakeMove()
        {
            //TODO: Логика хода
        }

        #endregion
    }
}
