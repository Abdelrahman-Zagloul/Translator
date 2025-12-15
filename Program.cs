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
            builder.Services.AddHttpClient<TranslatorService>((sp, client) =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config["OpenAI:Key"]);
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

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
            app.MapControllers();

            app.Run();
        }
    }
}
