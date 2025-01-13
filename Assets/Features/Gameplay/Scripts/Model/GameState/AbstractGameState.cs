namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Абстрактное состояние игры
    /// </summary>
    public abstract class AbstractGameState
    {
        #region Properties

        /// <summary>
        /// Заблокирована ли игра для взаимодействия
        /// </summary>
        public bool IsBlocked => stateType != GameStateType.WaitForTurn;

        /// <summary>
        /// Тип состояния игры
        /// </summary>
        public GameStateType StateType => stateType;
        protected GameStateType stateType = GameStateType.CheckStatus;

        protected GameStateController controller = default;

        #endregion

        #region Methods

        public AbstractGameState(GameStateController gameStateController, GameStateType gameStateType)
        {
            controller = gameStateController;
            stateType = gameStateType;
        }

        /// <summary>
        /// Возможно ли переключиться на заданное состояние
        /// </summary>
        /// <param name="nextState"></param>
        /// <returns></returns>
        public abstract bool CanSwitchToState(GameStateType nextState);

        /// <summary>
        /// Действия при входе в состояние
        /// </summary>
        public virtual void OnStateEnter() { }

        /// <summary>
        /// Действия при работе состояния
        /// </summary>
        public virtual void OnStateProcess() { }

        /// <summary>
        /// Действия при выходе из состояния
        /// </summary>
        public virtual void OnStateExit() { }

        #endregion
    }
}
