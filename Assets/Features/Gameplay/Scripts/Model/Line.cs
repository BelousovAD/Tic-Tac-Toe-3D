namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Линия
    /// </summary>
    public class Line : AbstractBallsContainer
    {
        #region Events

        /// <summary>
        /// Победитель найден
        /// </summary>
        public static event Action onWinnerFounded = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Победитель
        /// </summary>
        public override BallType Winner
        {
            get => winner;
            protected set
            {
                if (value != winner)
                {
                    winner = value;

                    if (winner != BallType.None)
                    {
                        onWinnerFounded();
                    }
                }
            }
        }

        private int _rank = 1;
        private Vector3Int _point = Vector3Int.zero;
        private Vector3Int _dirVector = Vector3Int.zero;
        private List<BallModel> _ballInfos = new();

        #endregion

        #region Methods

        public Line(Vector3Int point, Vector3Int dirVector, int rank)
        {
            _point = point;
            _rank = rank;
            _dirVector = dirVector;
            _ballInfos = new(_rank);
        }

        public override void TryAddBall(BallModel ball)
        {
            if (!IsFull && IsBallBelongingToLine(ball))
            {
                _ballInfos.Add(ball);
                UpdateFilledStatus(this);
                UpdateWinner(this);
            }
        }

        public override void Reset()
        {
            _ballInfos.Clear();
            Winner = BallType.None;
            IsFull = false;
        }

        protected override void UpdateWinner(AbstractBallsContainer ballsContainer)
        {
            if (IsFull)
            {
                BallType ballType = _ballInfos[0].BallType;

                foreach (BallModel ball in _ballInfos)
                {
                    if (ball.BallType != ballType)
                    {
                        return;
                    }
                }

                winner = ballType;
            }
        }

        protected override void UpdateFilledStatus(AbstractBallsContainer ballsContainer)
            => IsFull = _ballInfos.Count == _rank;

        protected virtual bool IsBallBelongingToLine(BallModel ball)
            => Vector3.Cross(ball.Position - _point, _dirVector) == Vector3.zero;

        #endregion
    }
}
