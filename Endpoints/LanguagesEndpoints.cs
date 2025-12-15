using Translator.DTOs;

namespace Translator.Endpoints
{

    public static class LanguagesEndpoints
    {
        public static IEndpointRouteBuilder MapLanguagesEndpoints(
            this IEndpointRouteBuilder app)
        {
            app.MapGet("/languages", () =>
            {
                return Results.Ok(SupportedLanguages.All);
            })
            .WithName("GetLanguages")
            .WithTags("Languages");

            return app;
        }
    }
}
