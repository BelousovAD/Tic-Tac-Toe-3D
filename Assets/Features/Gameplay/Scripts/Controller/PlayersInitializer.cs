namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Инициализатор игроков
    /// </summary>
    public class PlayersInitializer : MonoBehaviour
    {
        #region Constants

        protected const int PLAYERS_NUMBER = 2;

        #endregion

        #region Events

        /// <summary>
        /// Список игроков инициализирован
        /// </summary>
        public event Action onPlayersInited = delegate { };

        #endregion

        #region Properties

        [SerializeField]
        protected BallSpawnersController ballSpawnersController = default;

        /// <summary>
        /// Список игроков в порядке очереди хода
        /// </summary>
        public List<Player> Players
        {
            get => _players;
            protected set
            {
                if ((_players == null || _players.Count == 0) && value != null && value.Count > 0)
                {
                    _players = new(value);
                    onPlayersInited();
                }
            }
        }
        private List<Player> _players = new();

        protected List<BallSpawner> ballSpawners = new();

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(GameSettings gameSettings)
        {
            ballSpawners = ballSpawnersController.BallSpawners;
            List<Player> players = new(PLAYERS_NUMBER);

            if (gameSettings.GameMode == GameMode.WithFriend)
            {
                players.Add(new Player(ballSpawners[0], ballSpawners[0].BallData.Type.ToString()));
                players.Add(new Player(ballSpawners[1], ballSpawners[1].BallData.Type.ToString()));
            }
            else if (gameSettings.BallType == BallType.White)
            {
                players.Add(new Player(ballSpawners[0]));
                players.Add(new Bot(ballSpawners[1]));
            }
            else if (gameSettings.BallType == BallType.Black)
            {
                players.Add(new Bot(ballSpawners[0]));
                players.Add(new Player(ballSpawners[1]));
            }

            Players = players;
        }

        #endregion
    }
}
