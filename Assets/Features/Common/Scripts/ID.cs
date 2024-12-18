namespace TicTacToe3D.Features.Common
{
    using UnityEngine;

    /// <summary>
    /// Идентификатор
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ID), menuName = "TicTacToe3D/Features/Common/New " + nameof(ID))]
    public sealed class ID : ScriptableObject
    {
        #region Properties

        /// <summary>
        /// Значение
        /// </summary>
        public string Value => _value;
        [SerializeField]
        private string _value = string.Empty;

        #endregion
    }
}
