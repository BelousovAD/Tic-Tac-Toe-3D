namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Плоскость
    /// </summary>
    public class Plane : AbstractBallsContainer
    {
        #region Properties

        private int _rank = 1;
        private Vector3Int _start = Vector3Int.zero;
        private Vector3Int _firstDirVector = Vector3Int.zero;
        private Vector3Int _secondDirVector = Vector3Int.zero;
        private List<List<AbstractBallsContainer>> _emittedLines = new();

        #endregion

        #region Methods

        public Plane(
            Vector3Int start,
            Vector3Int firstDirVector,
            Vector3Int secondDirVecor,
            Vector3Int lineDirVector,
            int rank)
        {
            _rank = rank;
            _start = start;
            _firstDirVector = firstDirVector;
            _secondDirVector = secondDirVecor;
            _emittedLines = new List<List<AbstractBallsContainer>>(_rank);

            for (int i = 0; i < _rank; ++i)
            {
                _emittedLines.Add(new List<AbstractBallsContainer>(_rank));

                for (int j = 0; j < _rank; ++j)
                {
                    _emittedLines[i].Add(
                        new Line(
                            _start + _firstDirVector * i + _secondDirVector * j,
                            lineDirVector,
                            _rank)
                        );
                }
            }
        }

        public override void Dispose()
        {
            foreach (List<AbstractBallsContainer> row in _emittedLines)
            {
                foreach (AbstractBallsContainer line in row)
                {
                    line.Dispose();
                }
                row.Clear();
            }
            _emittedLines.Clear();
        }

        public override void AddBallModel(Ball ball)
        {
            AbstractBallsContainer container = GetContainerWithBallPosition(ball.Position);
            container.AddBallModel(ball);
        }

        public override Ball GetBallAt(Vector3Int position)
        {
            AbstractBallsContainer container = GetContainerWithBallPosition(position);
            return container.GetBallAt(position);
        }

        public override bool TryAddBall(Vector3Int position, BallType ballType)
        {
            bool result = false;

            if (!IsFull)
            {
                IsFull = true;

                foreach (List<AbstractBallsContainer> row in _emittedLines)
                {
                    foreach (AbstractBallsContainer line in row)
                    {
                        if (line.TryAddBall(position, ballType))
                        {
                            Version += 1;
                            result = true;
                        }

                        UpdateFilledStatus(line);
                        UpdateWinner(line);
                    }
                }

                if (!result)
                {
                    IsFull = result;
                }
            }

            return result;
        }

        protected virtual AbstractBallsContainer GetContainerWithBallPosition(Vector3Int position)
        {
            Vector3Int centeredBallPosition = position - _start;
            int i = Mathf.FloorToInt(Vector3.Dot(centeredBallPosition, _firstDirVector));
            int j = Mathf.FloorToInt(Vector3.Dot(centeredBallPosition, _secondDirVector));
            return _emittedLines[i][j];
        }

        #endregion
    }
}
