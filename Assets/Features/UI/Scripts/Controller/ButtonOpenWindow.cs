namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using UnityEngine;

    /// <summary>
    /// Кнопка открытия окна
    /// </summary>
    public class ButtonOpenWindow : AbstractButton
    {
        #region Properties

        [SerializeField]
        protected bool needCloseCurrent = false;
        [SerializeField]
        protected ID windowToOpenID = default;

        protected Window window = default;

        #endregion

        #region Methods

        public override void OnClick()
            => window.WindowsController.OpenNewWindow(windowToOpenID.Value, needCloseCurrent);

        protected override void Awake()
        {
            base.Awake();
            window = GetComponentInParent<Window>();
        }

        #endregion
    }
}
