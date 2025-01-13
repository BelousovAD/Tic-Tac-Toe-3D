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

        /// <summary>
        /// Клик по позиции спавна шара
        /// </summary>
        public static event Action onClick = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Позиция спавна шара
        /// </summary>
        public BallSpawnPosition BallSpawnPosition
        {
            get
            {
                if (ballSpawnPosition == null)
                {
                    ballSpawnPosition = GetComponent<BallSpawnPosition>();
                }

                return ballSpawnPosition;
            }
        }
        protected BallSpawnPosition ballSpawnPosition = default;

        #endregion

        #region Methods

        /// <summary>
        /// Действия при клике по позиции спавна шара
        /// </summary>
        public virtual void Click()
        {
            onBallSpawn(BallSpawnPosition);
            BallSpawnPosition.IncreaseNextPosition();
            onClick();
        }

        protected virtual void OnMouseUpAsButton()
        {
            if (isActiveAndEnabled && !EventSystem.current.IsPointerOverGameObject())
            {
                Click();
            }
        }

        #endregion
    }
}
