namespace TicTacToe3D.Features.Gameplay
{
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
        private List<List<Line>> _emittedLines = new();

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
            _emittedLines = new List<List<Line>>(_rank);

            for (int i = 0; i < _rank; ++i)
            {
                _emittedLines.Add(new List<Line>(_rank));

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

        public override void TryAddBall(BallModel ball)
        {
            if (!IsFull && Winner == BallType.None)
            {
                IsFull = true;

                foreach (List<Line> row in _emittedLines)
                {
                    foreach (Line line in row)
                    {
                        line.TryAddBall(ball);
                        UpdateFilledStatus(line);
                        UpdateWinner(line);
                    }
                }
            }
        }

        #endregion
    }
}
