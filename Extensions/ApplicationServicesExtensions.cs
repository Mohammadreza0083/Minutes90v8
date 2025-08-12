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
            services.AddDbContext<AppDbContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection") ?? 
                                     Environment.GetEnvironmentVariable("DATABASE_URL") ??
                                     Environment.GetEnvironmentVariable("DefaultConnection");
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Database connection string is not configured");
                }
                
                opt.UseNpgsql(connectionString)
                    .EnableSensitiveDataLogging();
            });
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
