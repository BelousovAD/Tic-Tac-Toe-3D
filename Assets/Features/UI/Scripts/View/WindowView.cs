namespace TicTacToe3D.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Отображение окна
    /// </summary>
    [RequireComponent(typeof(Window))]
    public class WindowView : MonoBehaviour
    {
        #region Properties

        protected Window window = default;

        #endregion

        #region Methods

        protected virtual void Awake()
            => window = GetComponent<Window>();

        protected virtual void Start()
        {
            window.onVisibleChanged += UpdateView;
            UpdateView();
        }

        protected virtual void OnDestroy()
            => window.onVisibleChanged -= UpdateView;

        protected virtual void UpdateView()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(window.IsVisible);
            }
        }

        #endregion
    }
}
