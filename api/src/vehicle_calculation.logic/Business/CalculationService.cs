using Microsoft.Extensions.Logging;
using vehicle_calculation.common.Action;
using vehicle_calculation.common.Enums;
using vehicle_calculation.logic.interfaces.Business;
using vehicle_calculation.logic.models.Vehicle;

namespace vehicle_calculation.logic.Business
{
    public class CalculationService(ILogger<CalculationService> logger) : ICalculationService
    {
        private readonly ILogger<CalculationService> _logger = logger;
        public ValueTask<DataResult<VehicleCalculationServiceModel>> CalculateAsync(VehicleServiceModel model, CancellationToken token = default)
        {
            var calculation = new VehicleCalculationServiceModel(model.BasePrice, model.VehicleType);
            var response = DataResult<VehicleCalculationServiceModel>.Success(calculation);

            // Basic and Special Fee Calculations
            var internalBasicFee = model.BasePrice * 0.10m;
            switch (model.VehicleType)
            {
                case VehicleTypeEnum.Common:
                    calculation.BasicFee = Math.Clamp(internalBasicFee, 10, 50);
                    calculation.SpecialFee = model.BasePrice * 0.02m;
                    break;
                case VehicleTypeEnum.Luxury:
                    calculation.BasicFee = Math.Clamp(internalBasicFee, 25, 200);
                    calculation.SpecialFee = model.BasePrice * 0.04m;
                    break;
                default:
                    var error = $"Unhandled VehicleTypeEnum for value: {model.VehicleType}";
                    _logger.LogError(error);
                    response.SetError(error);
                    return ValueTask.FromResult(response);
            }

            // Calculate Association Fee
            calculation.AssociationFee = model.BasePrice switch
            {
                <= 500 => 5,
                <= 1000 => 10,
                <= 3000 => 15,
                _ => 20
            };

            return ValueTask.FromResult(response);
        }
    }
}
