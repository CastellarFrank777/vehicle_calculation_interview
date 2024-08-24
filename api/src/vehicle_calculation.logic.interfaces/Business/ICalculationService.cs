using vehicle_calculation.common.Action;
using vehicle_calculation.logic.models.Vehicle;

namespace vehicle_calculation.logic.interfaces.Business
{
    public interface ICalculationService
    {
        ValueTask<DataResult<VehicleCalculationServiceModel>> CalculateAsync(VehicleServiceModel serviceModel, CancellationToken token = default);
    }
}
