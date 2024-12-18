namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Заблокированное состояние игрового поля
    /// </summary>
    public class BlockedGameAreaState : AbstractGameAreaState
    {
        #region Methods

        public BlockedGameAreaState() : base(GameAreaStateType.Blocked) { }

        public override bool CanSwitchToState(GameAreaStateType nextState)
            => nextState == GameAreaStateType.Default;

        #endregion
    }
}
