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

        #endregion

        #region Properties

        protected Text textField = default;
        protected TurnController turnController = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(TurnController _turnController)
        {
            turnController = _turnController;
            turnController.onTurnChanged += UpdateView;
        }

        protected virtual void Awake()
            => textField = GetComponent<Text>();

        protected virtual void OnDestroy()
            => turnController.onTurnChanged -= UpdateView;

        protected virtual void UpdateView()
        {
            textField.text = TURN_PREFIX + turnController.CurrentPlayer.Name;
        }

        #endregion
    }
}
