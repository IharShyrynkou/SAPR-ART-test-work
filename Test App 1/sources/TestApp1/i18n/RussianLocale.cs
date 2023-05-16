namespace TestApp1.i18n
{
    public class RussianLocale : ILocale
    {
        public string ColorButtonLabelText => "Образец цвета";
        public string ColorButtonText => "Кликните, чтобы начать";

        public string IgnoreThisColorButtonText => "Игнорировать этот цвет";
        public string StopIgnoreThisColorButtonText => "Не игнорировать этот цвет";

        public string ThisColorIgnoredButtonText => "Цвет игнорирован.";
        public string ThisColorNotIgnoredButtonText => "Этот цвет не игнорируется";

        public string ColorListLabelText => "Список неигнорируемых цветов";
        public string IgnoredColorListLabelText => "Список игнорируемых цветов";
    }
}