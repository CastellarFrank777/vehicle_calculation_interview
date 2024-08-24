using Microsoft.Extensions.DependencyInjection;
using vehicle_calculation.common.ErrorHandling;

namespace vehicle_calculation.common.Extensions
{
    public static class LogExtensions
    {
        public static void AddLogManager(this IServiceCollection services)
        {
            services.AddTransient<ILogManager, LogManager>();
        }
    }
}
