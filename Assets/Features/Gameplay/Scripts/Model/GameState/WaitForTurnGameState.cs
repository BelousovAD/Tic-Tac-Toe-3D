namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Состояние игры - ожидание хода
    /// </summary>
    public class WaitForTurnGameState : AbstractGameState
    {
        #region Methods

        public WaitForTurnGameState(GameStateController gameStateController)
            : base(gameStateController, GameStateType.WaitForTurn)
            => BallSpawnPositionController.onClick += SetCheckStatusState;

        ~WaitForTurnGameState()
            => BallSpawnPositionController.onClick -= SetCheckStatusState;

        public override bool CanSwitchToState(GameStateType nextState)
            => nextState == GameStateType.CheckStatus;

        protected virtual void SetCheckStatusState()
            => controller.SetState(GameStateType.CheckStatus);

        #endregion
    }
}
