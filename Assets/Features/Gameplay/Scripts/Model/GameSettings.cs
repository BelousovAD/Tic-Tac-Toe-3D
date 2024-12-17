namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Настройки игры
    /// </summary>
    public class GameSettings
    {
        #region Properties

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
        public BallType BallType
        {
            get => _ballType;
            set => _ballType = value;
        }
        private BallType _ballType = BallType.White;

        /// <summary>
        /// Ранг куба игрового поля
        /// </summary>
        public int Rank
        {
            get => _rank;
            set => _rank = value <= 0 ? 1 : value;
        }
        private int _rank = 3;

        #endregion
    }
}
