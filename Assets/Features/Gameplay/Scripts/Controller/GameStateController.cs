namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using Zenject;

    /// <summary>
    /// Контроллер состояний игры
    /// </summary>
    public class GameStateController
    {
        #region Events

        /// <summary>
        /// Состояние изменилось
        /// </summary>
        public event Action onStateChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public AbstractGameState CurrentState
        {
            get => currentState;
            protected set
            {
                if (value != currentState)
                {
                    currentState = value;
                    onStateChanged();
                }
            }
        }

        protected AbstractGameState currentState = default;

        protected List<AbstractGameState> states = new();

        #endregion

        #region Methods

        [Inject]
        public GameStateController(Cube cube, GameSettings gameSettings)
        {
            states = new()
            {
                new WaitForTurnGameState(this),
                new CheckStatusGameState(this, cube, gameSettings),
                new WinGameState(this),
                new LoseGameState(this),
            };
            CurrentState = states[0];
            CurrentState.OnStateEnter();
        }

        /// <summary>
        /// Установить состояние
        /// </summary>
        /// <param name="nextState"></param>
        public virtual void SetState(GameStateType nextState)
        {
            if (CurrentState.CanSwitchToState(nextState))
            {
                CurrentState.OnStateExit();
                CurrentState = states.Find(x => x.StateType == nextState);
                CurrentState.OnStateEnter();
            }
        }

        #endregion
    }
}
