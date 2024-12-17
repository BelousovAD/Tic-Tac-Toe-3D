namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Контроллер спавнеров шаров
    /// </summary>
    public class BallSpawnersController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        protected TurnController turnController = default;

        /// <summary>
        /// Спавнеры шаров
        /// </summary>
        public List<BallSpawner> BallSpawners => _ballSpawners;
        [SerializeField]
        private List<BallSpawner> _ballSpawners = new();

        #endregion

        #region Methods

        protected virtual void Start()
        {
            BallSpawners.ForEach(x => x.gameObject.SetActive(false));
            BallSpawners[0].gameObject.SetActive(true);
        }

        protected virtual void OnEnable()
            => turnController.onTurnChanged += SwitchObjects;

        protected virtual void OnDisable()
            => turnController.onTurnChanged -= SwitchObjects;

        protected virtual void SwitchObjects()
        {
            BallSpawners.ForEach(x => x.gameObject.SetActive(false));
            turnController.CurrentPlayer.BallSpawner.gameObject.SetActive(true);
        }

        #endregion
    }
}
