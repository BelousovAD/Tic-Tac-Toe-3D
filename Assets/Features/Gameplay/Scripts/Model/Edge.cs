namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Ребро
    /// </summary>
    public class Edge : AbstractBallsContainer
    {
        #region Properties

        private int _rank = 1;
        private Vector3Int _start = Vector3Int.zero;
        private Vector3Int _dirVector = Vector3Int.zero;
        private List<AbstractBallsContainer> _emittedLines = new();

        #endregion

        #region Methods

        public Edge(
            Vector3Int start,
            Vector3Int dirVector,
            Vector3Int lineDirVector,
            int rank)
        {
            _start = start;
            _rank = rank;
            _dirVector = dirVector;
            _emittedLines = new List<AbstractBallsContainer>(_rank);
            for (int i = 0; i < _rank; ++i)
            {
                _emittedLines.Add(
                    new Line(
                        _start + _dirVector * i,
                        lineDirVector,
                        _rank)
                    );
            }
        }

        public override bool TryAddBall(Ball ball)
        {
            bool result = false;

            if (!IsFull && Winner == BallType.None)
            {
                IsFull = true;

                foreach (AbstractBallsContainer line in _emittedLines)
                {
                    if (line.TryAddBall(ball))
                    {
                        UpdateFilledStatus(line);
                        UpdateWinner(line);
                        Version += 1;
                        result = true;
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
