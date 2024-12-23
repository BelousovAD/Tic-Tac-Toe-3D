namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using TicTacToe3D.Features.Gameplay;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Zenject;

    /// <summary>
    /// Кнопка вращения игрового поля
    /// </summary>
    public class ButtonGameAreaRotate : AbstractButton
    {
        #region Properties

        [SerializeField]
        protected MoveDirection rotateDirection = MoveDirection.None;

        protected GameAreaRotator gameAreaRotator = default;

        #endregion

        #region Methods

        public override void OnClick()
            => gameAreaRotator.Rotate(rotateDirection);

        [Inject]
        protected virtual void Construct(GameAreaRotator _gameAreaRotator)
            => gameAreaRotator = _gameAreaRotator;

        #endregion
    }
}
