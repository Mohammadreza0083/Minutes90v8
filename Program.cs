
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using minutes90v8.Entities;
using minutes90v8.Entities.Roles;
using minutes90v8.Extensions;
using minutes90v8.Data;

namespace Minutes90v8
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting application...");
                
                var builder = WebApplication.CreateBuilder(args);

                // Configure for Railway
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                Console.WriteLine($"Using port: {port}");
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

                // Add services to the container.
                builder.Services.AddApplicationServices(builder.Configuration);
                builder.Services.AddIdentityServices(builder.Configuration);

                SwaggerServicesExtensions.AddOpenApi(builder.Services);
                builder.Services.AddControllers();

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

                Console.WriteLine("Starting database migration...");

                using IServiceScope scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                try
                {
                    AppDbContext context = services.GetRequiredService<AppDbContext>();
                    await context.Database.MigrateAsync();
                    Console.WriteLine("Database migration completed");
                    
                    var userManager = services.GetRequiredService<UserManager<AppUsers>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
                    await IdentityDataSeederExtension.SeedUsersAndRolesAsync(userManager, roleManager);
                    Console.WriteLine("Database seeding completed");
                }
                catch (Exception ex)
                {
                    ILogger logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database or seeding data.");
                    Console.WriteLine($"Database error: {ex.Message}");
                }

                Console.WriteLine("Starting application...");
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application startup failed: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
