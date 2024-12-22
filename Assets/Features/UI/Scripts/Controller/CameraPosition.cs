namespace TicTacToe3D.Features.UI
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контроллер позиции камеры
    /// </summary>
    public class CameraPosition : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        private Vector2 _referenceAspect = new Vector2(9, 16);
        private Vector3 _defaultPosition = new Vector3(0f, Mathf.Tan(Mathf.PI / 4.5f) * 8f + 1.44f, -8f);
        private Vector3 _defaultRotation = new Vector3(40, 0, 0);

        #endregion

        #region Methods

        protected virtual void OnEnable()
        {
            Canvas.preWillRenderCanvases += SetPosition;
            SetPosition();
        }

        protected virtual void OnDisable()
            => Canvas.preWillRenderCanvases -= SetPosition;

        private void SetPosition()
        {
            float scaleFactor = GetScaleFactor();
            gameObject.transform.position = new Vector3(
                _defaultPosition.x,
                _defaultPosition.y * scaleFactor,
                _defaultPosition.z * scaleFactor
                );
            gameObject.transform.eulerAngles = _defaultRotation;
        }

        private float GetScaleFactor()
        {
            Vector2 screenSize = new(Display.main.renderingWidth, Display.main.renderingHeight);

            float scaleFactor = 1f / MathF.Min(1f, screenSize.x / screenSize.y * _referenceAspect.y / _referenceAspect.x);
            return scaleFactor;
        }

        #endregion
    }
}
