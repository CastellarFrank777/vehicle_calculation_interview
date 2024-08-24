using vehicle_calculation.api.models.Request;
using vehicle_calculation.logic.models.Vehicle;

namespace vehicle_calculation.mappings.Extensions
{
    public static class MappingExtensions
    {
        public static VehicleServiceModel ToServiceModel(this VehicleCalculationRequest request)
        {
            return new VehicleServiceModel(
                BasePrice: request.BasePrice.GetValueOrDefault(),
                VehicleType: request.VehicleType.GetValueOrDefault()
            );
        }
    }
}
