namespace TicTacToe3D.Features.Gameplay
{
    using System.Collections.Generic;
    using UnityEngine;
    using Zenject;

    /// <summary>
    /// Отображение слоя позиций спавна шаров
    /// </summary>
    public class LayerBallSpawnPositionsView : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        protected TurnController turnController = default;
        [SerializeField]
        protected List<GameObject> switchObjects = new();

        protected GameStateController gameStateController = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(GameStateController _gameStateController)
        {
            gameStateController = _gameStateController;
            gameStateController.onStateChanged += SwitchObjects;
        }

        protected virtual void OnDestroy()
            => gameStateController.onStateChanged -= SwitchObjects;

        protected virtual void SwitchObjects()
            => switchObjects.ForEach(
                x => x.SetActive(
                !gameStateController.CurrentState.IsBlocked
                && turnController.CurrentPlayer is not Bot
                )
            );

        #endregion
    }
}
