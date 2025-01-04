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
        #region Properties

        [SerializeField]
        protected string turnPrefixKey = string.Empty;
        [SerializeField]
        protected string winPostfixKey = string.Empty;
        [SerializeField]
        protected string winKey = string.Empty;
        [SerializeField]
        protected string loseKey = string.Empty;

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
            textField = GetComponent<Text>();
            gameSettings = _gameSettings;
            gameStateController = _gameStateController;
            gameStateController.onStateChanged += UpdateView;
            turnController = _turnController;
            turnController.onTurnPrepare += UpdateView;
            LocalizationManager.Instance.AddFunctionToChangeEvent(UpdateView);
            UpdateView();
        }

        protected virtual void OnDestroy()
        {
            gameStateController.onStateChanged -= UpdateView;
            turnController.onTurnPrepare -= UpdateView;

            if (LocalizationManager.Instance != null)
            {
                LocalizationManager.Instance.RemoveFunctionFromChangeEvent(UpdateView);
            }
        }

        protected virtual void UpdateView()
        {
            string currentPlayerName = turnController.CurrentPlayer.Name;

            textField.text = gameStateController.CurrentState.StateType switch
            {
                GameStateType.CheckStatus
                    => string.Empty,
                GameStateType.WaitForTurn
                    => LocalizationManager.Instance.GetStringFromCode(turnPrefixKey) + ": "
                    + LocalizationManager.Instance.GetStringFromCode(currentPlayerName, currentPlayerName),
                GameStateType.Win
                    => gameSettings.GameMode == GameMode.WithFriend
                    ? LocalizationManager.Instance.GetStringFromCode(currentPlayerName, currentPlayerName) + " " + LocalizationManager.Instance.GetStringFromCode(winPostfixKey) + "!"
                    : LocalizationManager.Instance.GetStringFromCode(winKey) + "!",
                GameStateType.Lose
                    => LocalizationManager.Instance.GetStringFromCode(loseKey) + "!",
                _
                    => string.Empty,
            };
        }

        #endregion
    }
}
