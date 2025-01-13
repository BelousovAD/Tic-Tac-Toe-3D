namespace TicTacToe3D.Features.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Выравниватель размера шрифта у группы текстовых компонентов
    /// </summary>
    public class FontSizeGroup : MonoBehaviour
    {
        #region Constants

        protected const int MAX_FONT_SIZE = 300;
        protected const int LOG_BASE = 2;

        #endregion

        #region Properties

        [SerializeField]
        protected List<Text> textFields = new();
        [SerializeField]
        protected CanvasScaler canvasScaler = default;
        [SerializeField]
        protected Canvas canvas = default;

        protected WaitForEndOfFrame wait = new();

        #endregion

        #region Methods

        protected virtual void OnEnable()
            => StartCoroutine(AdjustFontSize());

        protected virtual void OnDisable()
            => textFields.ForEach(x => x.resizeTextMaxSize = MAX_FONT_SIZE);

        protected virtual IEnumerator AdjustFontSize()
        {
            yield return wait;
            int groupFontSize = FindMinFontSizeFromGroup();
            textFields.ForEach(x => x.resizeTextMaxSize = groupFontSize);
        }

        protected virtual int FindMinFontSizeFromGroup()
        {
            int min = int.MaxValue;

            foreach (Text item in textFields)
            {
                min = Mathf.Min(Mathf.CeilToInt(item.cachedTextGenerator.fontSizeUsedForBestFit / CalculateScaleFactor()), min);
            }

            return min;
        }

        protected virtual float CalculateScaleFactor()
        {
            Vector2 screenSize = new(Screen.width, Screen.height);

            int displayIndex = canvas.targetDisplay;
            if (displayIndex > 0 && displayIndex < Display.displays.Length)
            {
                Display disp = Display.displays[displayIndex];
                screenSize = new(disp.renderingWidth, disp.renderingHeight);
            }

            float logWidth = Mathf.Log(screenSize.x / canvasScaler.referenceResolution.x, LOG_BASE);
            float logHeight = Mathf.Log(screenSize.y / canvasScaler.referenceResolution.y, LOG_BASE);
            float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, canvasScaler.matchWidthOrHeight);
            float scaleFactor = Mathf.Pow(LOG_BASE, logWeightedAverage);
            return scaleFactor;
        }

        #endregion
    }
}
