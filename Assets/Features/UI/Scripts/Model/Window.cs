namespace TicTacToe3D.Features.UI
{
    using System;
    using TicTacToe3D.Features.Common;
    using UnityEngine;

    /// <summary>
    /// Окно
    /// </summary>
    public class Window : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Видимость окна изменилась
        /// </summary>
        public event Action onVisibleChanged = delegate { };

        #endregion

        #region Properties

        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id => id.Value;
        [SerializeField]
        protected ID id = default;

        /// <summary>
        /// Является ли окно видимым
        /// </summary>
        public bool IsVisible
        {
            get => isVisible;
            set
            {
                if (value != isVisible)
                {
                    isVisible = value;
                    onVisibleChanged();
                }
            }
        }
        protected bool isVisible = false;

        #endregion
    }
}
