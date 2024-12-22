namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    /// <summary>
    /// Отображение очереди хода
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class TurnLabelView : MonoBehaviour
    {
        #region Constants

        protected const string TURN_PREFIX = "Turn: ";
        protected const string WIN_POSTFIX = " wins!";
        protected const string WIN = "Win!";
        protected const string LOSE = "Lose!";

        #endregion

        #region Properties

        protected Text textField = default;
        protected GameSettings gameSettings = default;
        protected GameStateController gameStateController = default;
        protected TurnController turnController = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(
            GameSettings _gameSettings,
            GameStateController _gameStateController,
            TurnController _turnController)
        {
            gameSettings = _gameSettings;
            gameStateController = _gameStateController;
            gameStateController.onStateChanged += UpdateView;
            turnController = _turnController;
            turnController.onTurnChanged += UpdateView;
        }

        protected virtual void Awake()
            => textField = GetComponent<Text>();

        protected virtual void OnDestroy()
        {
            gameStateController.onStateChanged -= UpdateView;
            turnController.onTurnChanged -= UpdateView;
        }

        protected virtual void UpdateView()
        {
            textField.text = gameStateController.CurrentState.StateType switch
            {
                GameStateType.CheckStatus=> string.Empty,
                GameStateType.WaitForTurn => TURN_PREFIX + turnController.CurrentPlayer.Name,
                GameStateType.Win => gameSettings.GameMode == GameMode.WithFriend? turnController.CurrentPlayer.Name + WIN_POSTFIX : WIN,
                GameStateType.Lose => LOSE,
                _ => string.Empty,
            };
        }

        #endregion
    }
}
