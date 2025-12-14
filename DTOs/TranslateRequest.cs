namespace Translator.DTOs
{
    public class TranslateRequest
    {
        public string Text { get; set; } = null!;
        public string TargetLanguage { get; set; } = null!;
    }
}
