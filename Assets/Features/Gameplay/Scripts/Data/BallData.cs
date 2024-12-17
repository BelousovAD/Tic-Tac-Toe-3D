namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Данные шара
    /// </summary>
    [CreateAssetMenu(fileName = nameof(BallData), menuName = "TicTacToe3D/Features/Gameplay/New " + nameof(BallData))]
    public class BallData : ScriptableObject
    {
        /// <summary>
        /// Тип шара
        /// </summary>
        public BallType Type => _type;
        [SerializeField]
        private BallType _type = BallType.Black;

        /// <summary>
        /// Префаб шара
        /// </summary>
        public BallView Prefab => _prefab;
        [SerializeField]
        private BallView _prefab = default;
    }
}
