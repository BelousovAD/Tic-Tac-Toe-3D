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
        #region Constants

        protected const int DEFAULT_PRIORITY = 1;
        protected const int DELTA_PRIORITY = 1;
        protected const int HIGH_PRIORITY = 30;

        #endregion

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

        /// <summary>
        /// Приоритет заполнения линии шарами
        /// </summary>
        public int Priority
        {
            get => _priority;
            protected set => _priority = value;
        }
        private int _priority = DEFAULT_PRIORITY;

        private int _rank = 1;
        private Vector3Int _point = Vector3Int.zero;
        private Vector3Int _dirVector = Vector3Int.zero;
        private List<Ball> _ballInfos = new();
        private string[] _ballTypes = Enum.GetNames(typeof(BallType));
        private Dictionary<string, int> _ballTypesCount = new();

        #endregion

        #region Methods

        public Line(Vector3Int point, Vector3Int dirVector, int rank)
        {
            _point = point;
            _rank = rank;
            _dirVector = dirVector;

            _ballInfos = new(_rank);
            for (int i = 0; i < _rank; ++i)
            {
                _ballInfos.Add(new Ball());
            }

            _ballTypesCount = new(_ballTypes.Length);
            foreach (string item in _ballTypes)
            {
                _ballTypesCount.Add(item, 0);
            }
            _ballTypesCount[BallType.None.ToString()] = _ballTypes.Length;
        }

        public override void Dispose()
        {
            _ballInfos.ForEach(x => x.onBallTypeChanged -= UpdatePriority);
            _ballInfos.Clear();
        }

        public override void AddBallModel(Ball ball)
        {
            if (IsBallBelongingToLine(ball.Position))
            {
                int i = CalculateBallIndex(ball.Position);
                _ballInfos[i] = ball;
                ball.LinkedLines.Add(this);
                ball.onBallTypeChanged += UpdatePriority;
            }
        }

        public override Ball GetBallAt(Vector3Int position)
        {
            if (IsBallBelongingToLine(position))
            {
                int i = CalculateBallIndex(position);
                return _ballInfos[i];
            }
            else
            {
                return null;
            }
        }

        public override bool TryAddBall(Vector3Int position, BallType ballType)
        {
            bool result = false;

            if (!IsFull)
            {
                Ball ball = GetBallAt(position);

                if (ball != null)
                {
                    ball.Type = ballType;
                    UpdateFilledStatus(this);
                    UpdateWinner(this);
                    Version += 1;
                    result = true;
                }
            }

            return result;
        }

        protected override void UpdateWinner(AbstractBallsContainer ballsContainer)
        {
            if (IsFull)
            {
                foreach (string item in _ballTypes)
                {
                    if (_ballTypesCount[item] == _rank)
                    {
                        _ballInfos.ForEach(x => x.IsHighlighted = true);
                        Winner = Enum.Parse<BallType>(item);
                    }
                }
            }
        }

        protected override void UpdateFilledStatus(AbstractBallsContainer ballsContainer)
            => IsFull = _ballTypesCount[BallType.None.ToString()] == 0;

        protected virtual void UpdatePriority(BallType addedBallType)
        {
            if (addedBallType == BallType.None)
            {
                throw new ArgumentException($"Тип шара не может быть равен {BallType.None} при обновлении приоритета линии");
            }

            _ballTypesCount[addedBallType.ToString()] += 1;
            _ballTypesCount[BallType.None.ToString()] -= 1;

            if (_ballTypesCount[addedBallType.ToString()] + _ballTypesCount[BallType.None.ToString()] < _rank)
            {
                Priority = DEFAULT_PRIORITY;
            }
            else if (_ballTypesCount[BallType.None.ToString()] > 1)
            {
                Priority += DELTA_PRIORITY;
            }
            else
            {
                Priority = HIGH_PRIORITY;
            }
        }

        protected virtual bool IsBallBelongingToLine(Vector3Int position)
            => Vector3.Cross(position - _point, _dirVector) == Vector3.zero;

        protected virtual int CalculateBallIndex(Vector3Int ballPosition)
            => Mathf.FloorToInt(Vector3.Dot(ballPosition - _point, _dirVector) / Vector3.Dot(_dirVector, _dirVector));

        #endregion
    }
}
