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
        protected List<GameObject> switchObjects = new();

        protected TurnController turnController = default;
        protected GameStateController gameStateController = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(GameStateController _gameStateController, TurnController _turnController)
        {
            gameStateController = _gameStateController;
            gameStateController.onStateChanged += SwitchObjects;
            turnController = _turnController;
            turnController.onTurnChanged += SwitchObjects;
            SwitchObjects();
        }

        protected virtual void OnDestroy()
        {
            gameStateController.onStateChanged -= SwitchObjects;
            turnController.onTurnChanged -= SwitchObjects;
        }

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
