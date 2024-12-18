namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Контроллер состояний игрового поля
    /// </summary>
    public class GameAreaStateController
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
        public AbstractGameAreaState CurrentState
        {
            get => _currentState;
            protected set
            {
                if (value != _currentState)
                {
                    _currentState = value;
                    onStateChanged();
                }
            }
        }
        private AbstractGameAreaState _currentState = default;

        protected List<AbstractGameAreaState> states = new();

        #endregion

        #region Methods

        public GameAreaStateController()
        {
            states = new()
            {
                new DefaultGameAreaState(),
                new BlockedGameAreaState(),
            };
            _currentState = states[0];
        }

        /// <summary>
        /// Установить состояние
        /// </summary>
        /// <param name="nextState"></param>
        public virtual void SetState(GameAreaStateType nextState)
        {
            if (CurrentState.CanSwitchToState(nextState))
            {
                CurrentState = states.Find(x => x.StateType == nextState);
            }
        }

        #endregion
    }
}
