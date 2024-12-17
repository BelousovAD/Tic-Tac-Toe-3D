namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Состояние игрового поля по умолчанию
    /// </summary>
    public class DefaultGameAreaState : AbstractGameAreaState
    {
        #region Methods

        public DefaultGameAreaState() : base(GameAreaStateType.Default) { }

        public override bool CanSwitchToState(GameAreaStateType nextState)
            => nextState == GameAreaStateType.Blocked;

        #endregion
    }
}
