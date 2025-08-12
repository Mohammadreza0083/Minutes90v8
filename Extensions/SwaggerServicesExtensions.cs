namespace minutes90.Extensions
{
    public static class SwaggerServicesExtensions
    {
        /// <summary>
        /// Adds Swagger services to the application's service collection.
        /// </summary>
        /// <param name="services">The service collection to which Swagger services will be added.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddOpenApi(this IServiceCollection services)
        {
            // Adds support for API endpoint exploration and documentation.
            services.AddEndpointsApiExplorer();

            // Configures Swagger to generate API documentation.
            services.AddSwaggerGen();

            return services;
        }
    }
}

