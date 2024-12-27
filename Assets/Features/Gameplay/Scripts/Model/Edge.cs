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

        public override void Dispose()
        {
            _emittedLines.ForEach(x => x.Dispose());
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

                foreach (AbstractBallsContainer line in _emittedLines)
                {
                    if (line.TryAddBall(position, ballType))
                    {
                        Version += 1;
                        result = true;
                    }

                    UpdateFilledStatus(line);
                    UpdateWinner(line);
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
            int index = Mathf.FloorToInt(Vector3.Dot(position - _start, _dirVector));
            return _emittedLines[index];
        }

        #endregion
    }
}
