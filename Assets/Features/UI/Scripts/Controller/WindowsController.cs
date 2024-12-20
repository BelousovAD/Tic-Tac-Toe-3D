namespace TicTacToe3D.Features.UI
{
    using System.Collections.Generic;
    using TicTacToe3D.Features.Common;
    using UnityEngine;

    /// <summary>
    /// Контроллер окон
    /// </summary>
    public class WindowsController : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        protected ID startWindowId = default;
        [SerializeField]
        protected List<Window> windowPrefabs = new();

        protected Stack<Window> windowsHistory = new();
        protected List<Window> spawnedWindows = new();

        #endregion

        #region Methods

        /// <summary>
        /// Открыть новое окно
        /// </summary>
        /// <param name="windowId"></param>
        public virtual void OpenNewWindow(string windowId, bool needCloseCurrent)
        {
            if (windowsHistory.Count > 0)
            {
                SetWindowStatus(windowsHistory.Peek(), !needCloseCurrent);
            }

            Window window = spawnedWindows.Find(x => x.Id == windowId);

            if (window != null)
            {
                SetWindowStatus(window, true);
            }
            else
            {
                window = SpawnWindow(windowId);

                if (window == null)
                {
                    Debug.LogError($"Невозможно открыть окно: Окно с идентификатором {windowId} не найдено в списке префабов окон");
                    return;
                }
            }

            window.transform.SetAsLastSibling();
            windowsHistory.Push(window);
        }

        /// <summary>
        /// Закрыть текущее окно
        /// </summary>
        public virtual void CloseCurrentWindow()
        {
            if (windowsHistory.Count > 1)
            {
                Window window = windowsHistory.Pop();
                SetWindowStatus(window, false);
                window = windowsHistory.Peek();
                SetWindowStatus(window, true);
            }
        }

        protected virtual void Start()
            => OpenNewWindow(startWindowId.Value, false);

        protected virtual Window SpawnWindow(string windowId)
        {
            Window window = windowPrefabs.Find(x => x.Id == windowId);

            if (window != null)
            {
                window = Instantiate(window, transform);
                window.Construct(this);
                spawnedWindows.Add(window);
                SetWindowStatus(window, true);
            }

            return window;
        }

        protected virtual void SetWindowStatus(Window window, bool status)
            => window.IsVisible = status;

        #endregion
    }
}
