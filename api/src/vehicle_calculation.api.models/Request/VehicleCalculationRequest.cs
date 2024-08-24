using vehicle_calculation.common.Enums;

namespace vehicle_calculation.api.models.Request
{
    public record VehicleCalculationRequest(decimal? BasePrice, VehicleTypeEnum? VehicleType);
}
