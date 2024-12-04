namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Куб
    /// </summary>
    public class Cube : AbstractBallsContainer
    {
        #region Constants

        private const int PLANES_NUMBER = 3;
        private const int EDGES_NUMBER = 6;
        private const int VERTICES_NUMBER = 4;

        #endregion

        #region Properties

        private int _rank = 1;
        private List<AbstractBallsContainer> _planes = new(PLANES_NUMBER);
        private List<AbstractBallsContainer> _edges = new(EDGES_NUMBER);
        private List<AbstractBallsContainer> _emittedLinesFromVertices = new(VERTICES_NUMBER);

        #endregion

        #region Methods

        public Cube(int rank)
        {
            _rank = rank;
            Vector3Int first = Vector3Int.right;
            Vector3Int second = Vector3Int.up;
            Vector3Int third = Vector3Int.forward;
            Vector3Int tmp;
            for (int i = 0; i < PLANES_NUMBER; ++i)
            {
                _planes.Add(
                    new Plane(
                        Vector3Int.zero,
                        first,
                        second,
                        third,
                        _rank)
                    );
                _edges.Add(
                    new Edge(
                        Vector3Int.zero,
                        first,
                        third + second,
                        _rank)
                    );
                _edges.Add(
                    new Edge(
                        Vector3Int.zero + second * (_rank - 1),
                        first,
                        third - second,
                        _rank)
                    );
                _emittedLinesFromVertices.Add(
                    new Line(
                        first * (_rank - 1),
                        third + second - first,
                        _rank)
                    );

                tmp = first;
                first = second;
                second = third;
                third = tmp;
            }

            _emittedLinesFromVertices.Add(
                new Line(
                    Vector3Int.zero,
                    third + second + first,
                    _rank)
                );
        }

        public override void TryAddBall(Ball ball)
        {
            if (!IsFull && Winner == BallType.None)
            {
                IsFull = true;

                TryAddBallIntoContainersList(_planes, ball);
                TryAddBallIntoContainersList(_edges, ball);
                TryAddBallIntoContainersList(_emittedLinesFromVertices, ball);
            }
        }

        public override void ResetToDefault()
        {
            _planes.ForEach(x => x.ResetToDefault());
            _edges.ForEach(x => x.ResetToDefault());
            _emittedLinesFromVertices.ForEach(x => x.ResetToDefault());
            Winner = BallType.None;
            IsFull = false;
        }

        protected virtual void TryAddBallIntoContainersList(List<AbstractBallsContainer> ballsContainers, Ball ball)
        {
            foreach (AbstractBallsContainer container in ballsContainers)
            {
                container.TryAddBall(ball);
                UpdateFilledStatus(container);
                UpdateWinner(container);
            }
        }

        #endregion
    }
}
