namespace TicTacToe3D.Features.UI
{
    using TicTacToe3D.Features.Common;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// Кнопка загрузки сцены
    /// </summary>
    public class LoadSceneButton : AbstractButton
    {
        #region Properties

        [SerializeField]
        protected string sceneToLoad = string.Empty;

        #endregion

        #region Methods

        public override void OnClick()
            => SceneManager.LoadScene(sceneToLoad);

        #endregion
    }
}
