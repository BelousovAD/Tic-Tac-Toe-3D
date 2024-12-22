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
        #region Properties

        [SerializeField]
        protected List<Text> textFields = new();

        protected WaitForEndOfFrame wait = new();

        #endregion

        #region Methods

        protected virtual void OnEnable()
            => StartCoroutine(AdjustFontSize());

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
                min = Mathf.Min(item.cachedTextGenerator.fontSizeUsedForBestFit, min);
            }

            return min;
        }

        #endregion
    }
}
