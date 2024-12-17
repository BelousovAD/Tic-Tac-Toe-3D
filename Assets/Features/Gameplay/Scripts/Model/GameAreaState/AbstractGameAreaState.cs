namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Абстрактное состояние игрового поля
    /// </summary>
    public abstract class AbstractGameAreaState
    {
        #region Properties

        /// <summary>
        /// Заблокировано ли игровое поле для взаимодействия
        /// </summary>
        public bool IsBlocked => stateType == GameAreaStateType.Blocked;

        /// <summary>
        /// Тип состояния игрового поля
        /// </summary>
        public GameAreaStateType StateType => stateType;
        protected GameAreaStateType stateType = GameAreaStateType.Default;

        #endregion

        #region Methods

        public AbstractGameAreaState(GameAreaStateType gameAreaStateType)
            => stateType = gameAreaStateType;

        /// <summary>
        /// Возможно ли переключиться на заданное состояние
        /// </summary>
        /// <param name="nextState"></param>
        /// <returns></returns>
        public abstract bool CanSwitchToState(GameAreaStateType nextState);

        #endregion
    }
}
