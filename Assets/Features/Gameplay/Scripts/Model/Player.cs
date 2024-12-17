namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {
        #region Properties

        /// <summary>
        /// Имя
        /// </summary>
        public string Name => _name;
        private string _name = string.Empty;

        /// <summary>
        /// Спавнер шаров
        /// </summary>
        public BallSpawner BallSpawner => _ballSpawner;
        private BallSpawner _ballSpawner = default;

        /// <summary>
        /// Контроллер очерёдности ходов
        /// </summary>
        public virtual TurnController TurnController
        {
            protected get => _turnController;
            set
            {
                if (value != _turnController)
                {
                    _turnController = value;
                }
            }
        }
        private TurnController _turnController = default;

        #endregion

        #region Methods

        public Player(BallSpawner ballSpawner, string name = null)
        {
            _ballSpawner = ballSpawner;
            _name = string.IsNullOrWhiteSpace(name) ? "Player" : name;
        }

        #endregion
    }
}
