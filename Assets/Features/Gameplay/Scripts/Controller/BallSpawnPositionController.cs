namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Контроллер позиции спавна шара
    /// </summary>
    [RequireComponent(typeof(BallSpawnPosition))]
    public class BallSpawnPositionController : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Запрос на спавн шара
        /// </summary>
        public static event Action<BallSpawnPosition> onBallSpawn = delegate { };

        #endregion

        #region Properties

        protected BallSpawnPosition ballSpawnPosition = default;

        #endregion

        #region Methods

        protected virtual void Awake() => ballSpawnPosition = GetComponent<BallSpawnPosition>();

        private void OnMouseUpAsButton()
        {
            if (!isActiveAndEnabled || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            onBallSpawn(ballSpawnPosition);
            ballSpawnPosition.IncreaseNextPosition();
        }

        #endregion
    }
}
