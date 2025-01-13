namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Состояние игры - победа
    /// </summary>
    public class WinGameState : AbstractGameState
    {
        #region Methods

        public WinGameState(GameStateController gameStateController)
            : base(gameStateController, GameStateType.Win)
        { }

        public override bool CanSwitchToState(GameStateType nextState)
            => false;

        #endregion
    }
}
