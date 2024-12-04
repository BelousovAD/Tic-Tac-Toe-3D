namespace TicTacToe3D.Features.Gameplay
{
    /// <summary>
    /// Игрок
    /// </summary>
    public class Player
    {
        #region Properties

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id => _id;
        private int _id = 0;

        /// <summary>
        /// Имя
        /// </summary>
        public string Name => _name;
        private string _name;
        private static int _idCount = 0;

        #endregion

        #region Methods

        public Player()
        {
            _id = _idCount++;
            _name = "Player_" + _id;
        }

        public Player(string name) : this() => _name = name;

        #endregion
    }
}
