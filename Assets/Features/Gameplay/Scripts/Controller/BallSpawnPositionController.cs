namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Zenject;

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

        /// <summary>
        /// Клик по позиции спавна шара
        /// </summary>
        public static event Action onClick = delegate { };

        #endregion

        #region Properties

        protected BallSpawnPosition ballSpawnPosition = default;

        #endregion

        #region Methods

        protected virtual void Awake()
            => ballSpawnPosition = GetComponent<BallSpawnPosition>();

        protected virtual void OnMouseUpAsButton()
        {
            if (!isActiveAndEnabled || EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            onClick();
            onBallSpawn(ballSpawnPosition);
            ballSpawnPosition.IncreaseNextPosition();
        }

        #endregion
    }
}
