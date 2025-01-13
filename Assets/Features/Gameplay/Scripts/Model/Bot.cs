namespace TicTacToe3D.Features.Gameplay
{
    using Cysharp.Threading.Tasks;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Бот
    /// </summary>
    public class Bot : Player
    {
        #region Constants

        private const int WAITING_MILLISECONDS = 2000;

        #endregion

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
                        TurnController.onTurnChanged -= MakeMoveAsync;
                    }

                    base.TurnController = value;

                    if (TurnController != null)
                    {
                        TurnController.onTurnChanged += MakeMoveAsync;
                    }
                }
            }
        }

        private BallSpawnPositionControllersProvider _ballSpawnPositionControllersProvider = default;
        private Cube _cube = default;

        #endregion

        #region Methods

        public Bot(
            BallSpawner ballSpawner,
            BallSpawnPositionControllersProvider ballSpawnPositionControllersProvider,
            Cube cube,
            string name = "Bot")
            : base(ballSpawner, name)
        {
            _ballSpawnPositionControllersProvider = ballSpawnPositionControllersProvider;
            _cube = cube;
        }

        protected async virtual void MakeMoveAsync()
        {
            if (CheckAccessToTurn())
            {
                await UniTask.Delay(WAITING_MILLISECONDS, true);
                MakeMove();
            }
        }

        protected virtual void MakeMove()
        {
            List<BallSpawnPositionController> topPriorityPositionControllers = new();
            Ball ballModel = null;
            int topPriority = 0;

            foreach (var positionController in _ballSpawnPositionControllersProvider.BallSpawnPositionControllers)
            {
                ballModel = _cube.GetBallAt(positionController.BallSpawnPosition.NextBallPosition);

                if (ballModel != null)
                {
                    if (ballModel.Priority == topPriority)
                    {
                        topPriorityPositionControllers.Add(positionController);
                    }
                    else if (ballModel.Priority > topPriority)
                    {
                        topPriority = ballModel.Priority;
                        topPriorityPositionControllers.Clear();
                        topPriorityPositionControllers.Add(positionController);
                    }
                }
            }

            if (topPriorityPositionControllers.Count > 0)
            {
                topPriorityPositionControllers[Random.Range(0, topPriorityPositionControllers.Count)].Click();
            }
            else
            {
                Debug.LogError($"{Name} не может сделать ход. Не найдена самая приоритетная позиция спавна шара");
            }
        }

        protected virtual bool CheckAccessToTurn()
            => TurnController.CurrentPlayer == this;

        #endregion
    }
}
