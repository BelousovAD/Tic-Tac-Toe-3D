namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Gameplay;
    using UnityEngine;
    using UnityEngine.UI;
    using Zenject;

    /// <summary>
    /// Отображение текущего шага вращения игрового поля
    /// </summary>
    [RequireComponent(typeof(Text))]
    public class GameAreaRotationStepView : MonoBehaviour
    {
        #region Constants

        protected const string SIGN_DEGREE = "°";

        #endregion

        #region Properties

        protected Text textField = default;
        protected GameAreaRotator gameAreaRotator = default;

        #endregion

        #region Methods

        [Inject]
        protected virtual void Construct(GameAreaRotator _gameAreaRotator)
        {
            textField = GetComponent<Text>();
            gameAreaRotator = _gameAreaRotator;
            gameAreaRotator.OnStepChanged += UpdateView;
            UpdateView();
        }

        protected virtual void OnDestroy()
            => gameAreaRotator.OnStepChanged -= UpdateView;

        protected virtual void UpdateView()
            => textField.text = gameAreaRotator.CurrentStep.ToString() + SIGN_DEGREE;

        #endregion
    }
}
