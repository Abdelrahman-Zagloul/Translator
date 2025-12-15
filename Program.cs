using System.Net.Http.Headers;
using Translator.Endpoints;
using Translator.Services;

namespace Translator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // configure HttpClient for TranslatorService
            builder.Services.AddHttpClient<ITranslatorService, TranslatorService>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config["OpenAI:Key"]);
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyCORS", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors("MyCORS");
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "An unexpected error occurred"
                    });
                });
            });


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapTranslateEndpoints();
            app.MapLanguagesEndpoints();

            app.MapControllers();
            app.Run();
        }
    }
}
