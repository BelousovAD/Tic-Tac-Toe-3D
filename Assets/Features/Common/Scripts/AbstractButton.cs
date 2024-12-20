namespace TicTacToe3D.Features.Common
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Абстрактная кнопка
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButton : MonoBehaviour
    {
        #region Properties

        private Button _button = default;

        #endregion

        #region Methods

        protected virtual void Awake()
            => _button = GetComponent<Button>();

        protected virtual void OnEnable()
            => _button.onClick.AddListener(OnClick);

        protected virtual void OnDisable()
            => _button.onClick.RemoveListener(OnClick);

        /// <summary>
        /// Действия при клике на кнопку
        /// </summary>
        public abstract void OnClick();

        #endregion
    }
}
