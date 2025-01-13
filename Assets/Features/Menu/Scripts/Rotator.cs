namespace TicTacToe3D.Features.Menu
{
    using UnityEngine;

    /// <summary>
    /// Вращатель
    /// </summary>
    public class Rotator : MonoBehaviour
    {
        #region Properties

        [SerializeField]
        protected Vector3 rotationSpeed = Vector3.zero;

        #endregion

        #region Methods

        protected virtual void FixedUpdate()
            => transform.Rotate(rotationSpeed * Time.fixedDeltaTime);

        #endregion
    }
}
