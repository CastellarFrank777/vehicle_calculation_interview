namespace vehicle_calculation.api.Extensions
{
    public static class CorsExtensions
    {
        public const string DefaultPolicy = "DefaultCORSPolicy";
        public static void AddCorsPolicies(this IServiceCollection services, IConfigurationManager config)
        {
            var origins = config.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(options =>
            {
                options.AddPolicy("DefaultPolicy",
                    builder =>
                    {
                        builder.WithOrigins(origins!)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });
        }

        public static void UseCorsPolicies(this WebApplication app)
        {
            app.UseCors(DefaultPolicy);
        }
    }
}
