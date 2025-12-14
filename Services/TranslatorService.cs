using Newtonsoft.Json;
using System.Text;
using Translator.DTOs;

namespace Translator.Services
{
    public class TranslatorService
    {
        private readonly HttpClient _httpClient;

        public TranslatorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> TranslateAsync(TranslateRequest request)
        {
            try
            {
                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new object[]
                    {
                    new { role = "system", content = $"You are a translator. Translate all user text to {request.TargetLanguage}." },
                    new { role = "user", content = request.Text }
                    },
                    temperature = 0,
                    max_tokens = 256
                };
                string jsonPayload = JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenAIResponse>(responseContent);

                return result?.choices?.FirstOrDefault()?.message?.content;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
