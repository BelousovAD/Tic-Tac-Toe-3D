namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

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

        private GameSettings _gameSettings = default;
        private List<AbstractBallsContainer> _planes = new(PLANES_NUMBER);
        private List<AbstractBallsContainer> _edges = new(EDGES_NUMBER);
        private List<AbstractBallsContainer> _emittedLinesFromVertices = new(VERTICES_NUMBER);

        #endregion

        #region Methods

        [Inject]
        public Cube(GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
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
                        _gameSettings.Rank)
                    );
                _edges.Add(
                    new Edge(
                        Vector3Int.zero,
                        first,
                        third + second,
                        _gameSettings.Rank)
                    );
                _edges.Add(
                    new Edge(
                        Vector3Int.zero + second * (_gameSettings.Rank - 1),
                        first,
                        third - second,
                        _gameSettings.Rank)
                    );
                _emittedLinesFromVertices.Add(
                    new Line(
                        first * (_gameSettings.Rank - 1),
                        third + second - first,
                        _gameSettings.Rank)
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
                    _gameSettings.Rank)
                );
        }

        public override void Dispose()
        {
            _planes.ForEach(x => x.Dispose());
            _planes.Clear();
            _edges.ForEach(x => x.Dispose());
            _edges.Clear();
            _emittedLinesFromVertices.ForEach(x => x.Dispose());
            _emittedLinesFromVertices.Clear();
        }

        public override bool TryAddBall(Ball ball)
        {
            bool result = false;

            if (!IsFull && Winner == BallType.None)
            {
                IsFull = true;

                bool planesResult = TryAddBallIntoContainersList(_planes, ball);
                bool edgesResult = TryAddBallIntoContainersList(_edges, ball);
                bool verticesResult = TryAddBallIntoContainersList(_emittedLinesFromVertices, ball);

                if (planesResult || edgesResult || verticesResult)
                {
                    Version += 1;
                    result = true;
                }
            }

            return result;
        }

        protected virtual bool TryAddBallIntoContainersList(List<AbstractBallsContainer> ballsContainers, Ball ball)
        {
            bool result = false;

            foreach (AbstractBallsContainer container in ballsContainers)
            {
                if (container.TryAddBall(ball))
                {
                    UpdateFilledStatus(container);
                    UpdateWinner(container);
                    result = true;
                }
            }

            return result;
        }

        #endregion
    }
}
