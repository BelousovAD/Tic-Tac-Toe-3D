namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Контроллер очерёдности ходов
    /// </summary>
    public class TurnController : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Текущий ход изменился
        /// </summary>
        public event Action onTurnChanged = delegate { };

        #endregion

        #region Properties

        [SerializeField]
        protected PlayersInitializer playersInitializer = default;

        /// <summary>
        /// Игрок, до которого дошла очередь хода
        /// </summary>
        public Player CurrentPlayer
        {
            get => _currentPlayer;
            protected set
            {
                if (value != _currentPlayer)
                {
                    _currentPlayer = value;
                    onTurnChanged();
                }
            }
        }
        private Player _currentPlayer = default;

        [Inject]
        protected GameStateController gameStateController = default;
        protected List<Player> players = null;
        protected IEnumerator<Player> playersEnumerator = default;

        #endregion

        #region Methods

        protected virtual void OnEnable()
        {
            playersInitializer.onPlayersInited += UpdatePlayers;
            UpdatePlayers();

            gameStateController.onStateChanged += MoveToNextPlayer;
            MoveToNextPlayer();
        }

        protected virtual void OnDisable()
        {
            playersInitializer.onPlayersInited -= UpdatePlayers;
            gameStateController.onStateChanged -= MoveToNextPlayer;
        }

        protected virtual void UpdatePlayers()
        {
            if (playersInitializer.Players != null && playersInitializer.Players.Count > 0)
            {
                players = playersInitializer.Players;
                playersEnumerator = players.GetEnumerator();
                players.ForEach(x => x.TurnController = this);
            }
        }

        protected virtual void MoveToNextPlayer()
        {
            if (gameStateController.CurrentState?.StateType == GameStateType.WaitForTurn)
            {
                if (!playersEnumerator.MoveNext())
                {
                    playersEnumerator.Reset();
                    playersEnumerator.MoveNext();
                }

                CurrentPlayer = playersEnumerator.Current;
            }
        }

        #endregion
    }
}
