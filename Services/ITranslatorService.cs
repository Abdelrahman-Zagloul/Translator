using Translator.DTOs;

namespace Translator.Services
{
    public interface ITranslatorService
    {
        Task<string?> TranslateAsync(TranslateRequest request);
    }
}
