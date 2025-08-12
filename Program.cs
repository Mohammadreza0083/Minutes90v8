
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Minutes90v8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting application...");
                
                var builder = WebApplication.CreateBuilder(args);

                // Configure for Railway
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                Console.WriteLine($"Using port: {port}");
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

                // Add basic services only
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();

                var app = builder.Build();

                Console.WriteLine("Application built successfully");

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(opt =>
                    {
                        opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();

                Console.WriteLine("Starting web server...");
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application startup failed: {ex.Message}");
                throw;
            }
        }
    }
}
