namespace TicTacToe3D.Features.Localization
{
    using TicTacToe3D.Features.Common;

    /// <summary>
    /// Кнопка переключения языка
    /// </summary>
    public class SwitchLanguageButton : AbstractButton
    {
        public override void OnClick()
        {
            if (LocalizationManager.Instance != null)
            {
                LocalizationManager.Instance.SwitchCurrentLocalizationData();
            }
        }
    }
}
