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

            for (int i = 0; i < _gameSettings.Rank; ++i)
            {
                for (int j = 0; j < _gameSettings.Rank; ++j)
                {
                    for (int k = 0; k < _gameSettings.Rank; ++k)
                    {
                        AddBallModel(new Ball(new Vector3Int(i, j, k), BallType.None));
                    }
                }
            }
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

        public override void AddBallModel(Ball ball)
        {
            _planes.ForEach(x => x.AddBallModel(ball));
            _edges.ForEach(x => x.AddBallModel(ball));
            _emittedLinesFromVertices.ForEach(x => x.AddBallModel(ball));
        }

        public override Ball GetBallAt(Vector3Int position)
            => _planes[0].GetBallAt(position);

        public override bool TryAddBall(Vector3Int position, BallType ballType)
        {
            bool result = false;

            if (!IsFull)
            {
                IsFull = true;

                bool planesResult = TryAddBallIntoContainersList(_planes, position, ballType);
                bool edgesResult = TryAddBallIntoContainersList(_edges, position, ballType);
                bool verticesResult = TryAddBallIntoContainersList(_emittedLinesFromVertices, position, ballType);

                if (planesResult || edgesResult || verticesResult)
                {
                    Version += 1;
                    result = true;
                }

                if (!result)
                {
                    isFull = result;
                }
            }

            return result;
        }

        protected virtual bool TryAddBallIntoContainersList(
            List<AbstractBallsContainer> ballsContainers,
            Vector3Int ballPosition,
            BallType ballType)
        {
            bool result = false;

            foreach (AbstractBallsContainer container in ballsContainers)
            {
                if (container.TryAddBall(ballPosition, ballType))
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
