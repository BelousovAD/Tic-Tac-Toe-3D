namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Отображение шара
    /// </summary>
    [RequireComponent(typeof(Outline))]
    public class BallView : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Шар
        /// </summary>
        public Ball Ball
        {
            get => ball;
            set
            {
                if (ball != null)
                {
                    ball.onHighlightChanged -= UpdateView;
                }

                ball = value;

                if (ball != null)
                {
                    ball.onHighlightChanged += UpdateView;
                    UpdateView();
                }
            }
        }
        protected Ball ball = default;

        protected Outline outline = default;

        #endregion

        #region Methods

        protected virtual void Awake()
            => outline = GetComponent<Outline>();

        protected virtual void OnEnable()
            => UpdateView();

        protected virtual void UpdateView()
            => outline.enabled = ball != null && ball.IsHighlighted;

        #endregion
    }
}
