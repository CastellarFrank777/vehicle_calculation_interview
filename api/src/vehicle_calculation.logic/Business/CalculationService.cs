using vehicle_calculation.common.Action;
using vehicle_calculation.common.Enums;
using vehicle_calculation.common.ErrorHandling;
using vehicle_calculation.logic.interfaces.Business;
using vehicle_calculation.logic.models.Vehicle;

namespace vehicle_calculation.logic.Business
{
    public class CalculationService : ICalculationService
    {
        private readonly ILogManager _logger;

        public CalculationService(ILogManager logger)
        {
            _logger = logger;
            _logger.SetLogType(typeof(CalculationService));
        }

        public ValueTask<DataResult<VehicleCalculationServiceModel>> CalculateAsync(VehicleServiceModel model, CancellationToken token = default)
        {
            var calculation = new VehicleCalculationServiceModel(model.BasePrice, model.VehicleType);
            var response = DataResult<VehicleCalculationServiceModel>.Success(calculation);
            if (model.BasePrice < 0)
            {
                response.SetError($"{nameof(model.BasePrice)} can't be lower than zero");
                return ValueTask.FromResult(response);
            }

            if (!Enum.IsDefined(typeof(VehicleTypeEnum), model.VehicleType))
            {
                response.SetError($"{nameof(model.VehicleType)} value: {model.VehicleType} is not a valid VehicleTypeEnum.");
                return ValueTask.FromResult(response);
            }

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
                    var (_, error) = _logger.LogError($"Unhandled VehicleTypeEnum for value: {model.VehicleType}");
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
