namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using UnityEngine;

    /// <summary>
    /// Кнопка закрытия окна
    /// </summary>
    public class ButtonCloseWindow : AbstractButton
    {
        #region Properties

        protected Window window = default;

        #endregion

        #region Methods

        public override void OnClick()
            => window.WindowsController.CloseCurrentWindow();

        protected override void Awake()
        {
            base.Awake();
            window = GetComponentInParent<Window>();
        }

        #endregion
    }
}
