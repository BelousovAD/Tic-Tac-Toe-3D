﻿namespace TicTacToe3D.Features.UI
{
    using UnityEngine;

    /// <summary>
    /// Контроллер позиции камеры
    /// </summary>
    public class CameraPosition : MonoBehaviour
    {
        // NOTE: Все значения подобраны методом эксперимента...

        #region Properties

        [SerializeField]
        private Vector2 _referenceAspect = new Vector2(9, 16);
        [SerializeField]
        private float _centerOfGameArea = 2.88f;
        
        private Vector3 _defaultRotation = new(30, 0, 0);
        private float _diagonalOfBase = 3.6f * Mathf.Sqrt(2f);
        private Vector3 _defaultPosition = Vector3.zero;

        #endregion

        #region Methods

        protected virtual void OnEnable()
        {
            Canvas.preWillRenderCanvases += SetPosition;
            CalculateDefaultPosition();
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

            float scaleFactor = 1f / Mathf.Min(1.3f, screenSize.x / screenSize.y * _referenceAspect.y / _referenceAspect.x);
            return scaleFactor;
        }

        private void CalculateDefaultPosition()
        {
            // NOTE: Расчёты позиции камеры верны только при угле обзора равном 60 градусов
            _defaultPosition.z = -_diagonalOfBase * Mathf.Sqrt(3f) / 1.1f;
            _defaultPosition.y = Mathf.Tan(_defaultRotation.x * Mathf.Deg2Rad) * Mathf.Abs(_defaultPosition.z) + _centerOfGameArea;
        }

        #endregion
    }
}
