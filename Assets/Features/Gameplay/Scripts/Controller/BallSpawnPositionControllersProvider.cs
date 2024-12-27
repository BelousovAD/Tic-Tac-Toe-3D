namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Провайдер контроллеров позиций спавна шара
    /// </summary>
    public class BallSpawnPositionControllersProvider : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Список контроллеров позиций спавна шара
        /// </summary>
        public List<BallSpawnPositionController> BallSpawnPositionControllers => ballSpawnPositionControllers;
        [SerializeField]
        protected List<BallSpawnPositionController> ballSpawnPositionControllers = new();

        #endregion
    }
}