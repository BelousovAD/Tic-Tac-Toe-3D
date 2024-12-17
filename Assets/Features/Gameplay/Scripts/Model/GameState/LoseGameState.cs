namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Состояние игры - проигрыш
    /// </summary>
    public class LoseGameState : AbstractGameState
    {
        #region Methods

        public LoseGameState(GameStateController gameStateController)
            : base(gameStateController, GameStateType.Lose)
        { }

        public override bool CanSwitchToState(GameStateType nextState)
            => false;

        #endregion
    }
}
