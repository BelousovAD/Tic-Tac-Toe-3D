using Zenject;

namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Состояние проверки статуса игры
    /// </summary>
    public class CheckStatusGameState : AbstractGameState
    {
        #region Properties

        protected Cube cube = default;
        protected GameSettings gameSettings = default;
        protected int lastBallsContainerVersion = 0;

        #endregion

        #region Methods

        [Inject]
        public CheckStatusGameState(GameStateController gameStateController, Cube _cube, GameSettings _gameSettings)
            : base(gameStateController, GameStateType.CheckStatus)
        {
            cube = _cube;
            gameSettings = _gameSettings;
        }

        public override bool CanSwitchToState(GameStateType nextState)
            => nextState != GameStateType.CheckStatus;

        public override void OnStateEnter()
        {
            cube.onVersionChanged += CheckStatus;
            CheckStatus();
        }

        public override void OnStateExit()
            => cube.onVersionChanged -= CheckStatus;

        protected virtual void CheckStatus()
        {
            if (cube.Version > lastBallsContainerVersion)
            {
                lastBallsContainerVersion = cube.Version;

                if (cube.Winner != BallType.None)
                {
                    if (gameSettings.GameMode == GameMode.WithBot && gameSettings.BallType != cube.Winner)
                    {
                        controller.SetState(GameStateType.Lose);
                    }
                    else
                    {
                        controller.SetState(GameStateType.Win);
                    }
                }
                else
                {
                    controller.SetState(GameStateType.WaitForTurn);
                }
            }
        }

        #endregion
    }
}
