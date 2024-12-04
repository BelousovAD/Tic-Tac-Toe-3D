namespace TicTacToe3D.Features.Gameplay
{
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Отображение позиции спавна шара
    /// </summary>
    [RequireComponent(typeof(BallSpawnPosition))]
    [RequireComponent(typeof(MeshRenderer))]
    public class BallSpawnPositionView : MonoBehaviour
    {
        #region Constants

        protected const string EMISSION = "_EMISSION";

        #endregion

        #region Properties

        protected BallSpawnPosition ballSpawnPosition = default;
        protected Material material = default;

        #endregion

        #region Methods

        protected virtual void Awake()
        {
            ballSpawnPosition = GetComponent<BallSpawnPosition>();
            material = GetComponent<MeshRenderer>().material;
            material.DisableKeyword(EMISSION);

            ballSpawnPosition.onVisibleChanged += UpdateView;
        }

        protected virtual void OnDestroy() => ballSpawnPosition.onVisibleChanged -= UpdateView;

        protected virtual void OnDisable() => material.DisableKeyword(EMISSION);

        protected virtual void OnMouseEnter()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            material.EnableKeyword(EMISSION);
        }

        protected virtual void OnMouseExit()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            material.DisableKeyword(EMISSION);
        }

        protected virtual void UpdateView() => gameObject.SetActive(ballSpawnPosition.IsVisible);

        #endregion
    }
}
