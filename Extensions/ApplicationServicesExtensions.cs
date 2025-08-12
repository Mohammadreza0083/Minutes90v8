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
                opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection") ?? Environment.GetEnvironmentVariable("DefaultConnection"))
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
