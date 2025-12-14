namespace Translator.DTOs
{

    public class OpenAIResponse
    {
        public List<Choice> choices { get; set; } = new List<Choice>();
    }
}
