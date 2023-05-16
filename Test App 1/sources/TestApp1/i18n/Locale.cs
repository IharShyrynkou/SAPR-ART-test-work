namespace TestApp1.i18n
{
    public interface ILocale
    {
        string ColorButtonLabelText { get; }
        string ColorButtonText { get; }

        string IgnoreThisColorButtonText { get; }
        string StopIgnoreThisColorButtonText { get; }

        string ThisColorIgnoredButtonText { get; }
        string ThisColorNotIgnoredButtonText { get; }

        string ColorListLabelText { get; }
        string IgnoredColorListLabelText { get; }
    }
}