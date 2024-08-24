using Microsoft.Extensions.DependencyInjection;
using vehicle_calculation.logic.Business;
using vehicle_calculation.logic.interfaces.Business;

namespace vehicle_calculation.logic.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddLogicDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICalculationService, CalculationService>();
        }
    }
}
