using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using minutes90v8.Data;
using minutes90v8.Helper;
using minutes90v8.Interfaces;
using minutes90v8.Repository;
using minutes90v8.Services;

namespace minutes90v8.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices
        (this IServiceCollection services, IConfiguration configuration)
        { 
            services.AddControllers().AddNewtonsoftJson(_ =>
            {
            });
            
            try
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                                     Environment.GetEnvironmentVariable("DATABASE_URL") ??
                                     Environment.GetEnvironmentVariable("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    Console.WriteLine("Warning: No database connection string found. Using in-memory database.");
                    services.AddDbContext<AppDbContext>(opt =>
                    {
                        opt.UseInMemoryDatabase("Minutes90Db");
                    });
                }
                else
                {
                    services.AddDbContext<AppDbContext>(opt =>
                    {
                        opt.UseNpgsql(connectionString)
                            .EnableSensitiveDataLogging();
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database configuration error: {ex.Message}. Using in-memory database.");
                services.AddDbContext<AppDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase("Minutes90Db");
                });
            }
            
            services.AddHttpClient();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
            services.AddScoped<IAccountServices, AccountServices>();
            services.AddScoped<IUnitOfWorkRepo, UnitOfWorkRepo>();
            services.AddScoped<ITokenServices, TokenServices>();
            services.AddCors();
            return services;
        }
    }
}
