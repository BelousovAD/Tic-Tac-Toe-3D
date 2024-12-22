namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Контроллер спавнеров шаров
    /// </summary>
    public class BallSpawnersController : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Спавнеры шаров
        /// </summary>
        public List<BallSpawner> BallSpawners => _ballSpawners;
        [SerializeField]
        private List<BallSpawner> _ballSpawners = new();

        protected TurnController turnController = default;

        #endregion

        #region Methods

        protected virtual void Start()
        {
            BallSpawners.ForEach(x => x.gameObject.SetActive(false));
            BallSpawners[0].gameObject.SetActive(true);
        }

        [Inject]
        protected virtual void Construct(TurnController _turnController)
        {
            turnController = _turnController;
            turnController.onTurnChanged += SwitchObjects;
        }

        protected virtual void OnDestroy()
            => turnController.onTurnChanged -= SwitchObjects;

        protected virtual void SwitchObjects()
        {
            BallSpawners.ForEach(x => x.gameObject.SetActive(false));
            turnController.CurrentPlayer.BallSpawner.gameObject.SetActive(true);
        }

        #endregion
    }
}
