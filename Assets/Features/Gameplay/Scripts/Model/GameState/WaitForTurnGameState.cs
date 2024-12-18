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
        { }

        public override bool CanSwitchToState(GameStateType nextState)
            => nextState == GameStateType.CheckStatus;

        public override void OnStateEnter()
            => BallSpawnPositionController.onClick += SetCheckStatusState;

        public override void OnStateExit()
            => BallSpawnPositionController.onClick -= SetCheckStatusState;

        protected virtual void SetCheckStatusState()
            => controller.SetState(GameStateType.CheckStatus);

        #endregion
    }
}
