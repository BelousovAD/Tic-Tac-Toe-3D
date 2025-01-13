namespace TicTacToe3D.Features.Gameplay
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Контроллер шара
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BallController : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Шар заснул
        /// </summary>
        public static event Action onBallSlept = delegate { };

        #endregion

        #region Properties

        protected Rigidbody ballRigidbody = default;

        #endregion

        #region Methods

        protected virtual void Awake() => ballRigidbody = GetComponent<Rigidbody>();

        private void FixedUpdate()
        {
            if (ballRigidbody.IsSleeping())
            {
                enabled = false;
                onBallSlept();
            }
        }

        #endregion
    }
}
