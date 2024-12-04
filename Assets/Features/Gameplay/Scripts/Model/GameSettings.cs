using UnityEngine;

namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Настройки игры
    /// </summary>
    public class GameSettings
    {
        /// <summary>
        /// Режим игры
        /// </summary>
        public GameMode GameMode
        {
            get => _gameMode;
            set => _gameMode = value;
        }
        private GameMode _gameMode = GameMode.WithFriend;

        /// <summary>
        /// Цвет шара
        /// </summary>
        public BallColor BallColor
        {
            get => _ballColor;
            set => _ballColor = value;
        }
        private BallColor _ballColor = BallColor.White;

        /// <summary>
        /// Ранг куба игрового поля
        /// </summary>
        public int Rank
        {
            get => _rank;
            set => _rank = value <= 0 ? 1 : value;
        }
        private int _rank = 3;
    }
}
