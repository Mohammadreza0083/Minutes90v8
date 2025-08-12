
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
                var builder = WebApplication.CreateBuilder(args);

                // Configure for Railway
                var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
                builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

                // Add basic services first
                builder.Services.AddControllers();
                SwaggerServicesExtensions.AddOpenApi(builder.Services);

                // Add database services
                try
                {
                    builder.Services.AddApplicationServices(builder.Configuration);
                    Console.WriteLine("Database services added successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database services error: {ex.Message}");
                }

                // Add Identity services
                try
                {
                    builder.Services.AddIdentityServices(builder.Configuration);
                    Console.WriteLine("Identity services added successfully");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Identity services error: {ex.Message}");
                }

                var app = builder.Build();

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

                // Try database operations
                try
                {
                    using IServiceScope scope = app.Services.CreateScope();
                    var services = scope.ServiceProvider;
                    
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
                    Console.WriteLine($"Database operation error: {ex.Message}");
                    // Continue without database operations
                }

                Console.WriteLine("Starting application...");
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
