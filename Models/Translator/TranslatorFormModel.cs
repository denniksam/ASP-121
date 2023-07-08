namespace ASP121.Models.Translator
{
    public class TranslatorFormModel
    {
        public String  LangFrom        { get; set; } = null!;
        public String  LangTo          { get; set; } = null!;
        public String  OriginalText    { get; set; } = null!;
        public String? TranslatedText  { get; set; }
        public String? TranslateButton { get; set; }
        public String? InverseButton   { get; set; }
    }
}
