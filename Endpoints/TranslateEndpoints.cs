using Microsoft.AspNetCore.Mvc;
using Translator.DTOs;
using Translator.Services;

namespace Translator.Endpoints
{
    public static class TranslateEndpoints
    {
        public static IEndpointRouteBuilder MapTranslateEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/translate", async (ITranslatorService translatorService, [FromBody] TranslateRequest request) =>
            {
                var result = await translatorService.TranslateAsync(request);

                return result == null ?
                    Results.BadRequest("Failed To Translate") :
                    Results.Ok(new { TranslatedText = result });
            });

            return app;
        }
    }
}
