namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;

    /// <summary>
    /// Вращатель игрового поля
    /// </summary>
    public class GameAreaRotator : MonoBehaviour
    {
        #region Constants

        private const float SENSITIVITY = 3f;

        #endregion

        #region Properties

        private float _rotation = 0f;

        #endregion

        #region Methods

        protected virtual void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _rotation = Input.GetAxis("Mouse X") * SENSITIVITY;
                transform.Rotate(Vector3.up, -_rotation, Space.World);
            }
        }

        #endregion
    }
}
