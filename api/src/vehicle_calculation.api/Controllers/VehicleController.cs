using System.Net;
using Microsoft.AspNetCore.Mvc;
using vehicle_calculation.api.models.Request;
using vehicle_calculation.api.models.Response;
using vehicle_calculation.common.Action;
using vehicle_calculation.common.Enums;
using vehicle_calculation.common.ErrorHandling;
using vehicle_calculation.logic.interfaces.Business;
using vehicle_calculation.mappings.Extensions;

namespace vehicle_calculation.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogManager _logger;
        private readonly ICalculationService _calculationService;

        public VehicleController(ILogManager logger,
            ICalculationService calculationService)
        {
            _logger = logger;
            _logger.SetLogType(typeof(VehicleController));

            _calculationService = calculationService;
        }

        [HttpPost]
        [Route("calculate")]
        public async Task<ActionResult<VehicleCalculationResponse>> Calculate(VehicleCalculationRequest request, CancellationToken token = default)
        {
            var validationResult = ValidateRequest(request);
            if (validationResult.IsError)
            {
                return BadRequest(validationResult.Error);
            }

            var serviceModel = request.ToServiceModel();
            var calculationResult = await _calculationService.CalculateAsync(serviceModel, token);
            if (calculationResult.IsError)
            {
                var (_, error) = _logger.LogError("Error at performing vehicle calculations.");
                return StatusCode((int)HttpStatusCode.InternalServerError, error);
            }

            return Ok(calculationResult.Data);
        }

        private DataResult<string> ValidateRequest(VehicleCalculationRequest request)
        {
            var result = DataResult<string>.Success();
            if (!request.VehicleType.HasValue)
            {
                result.SetError($"{nameof(request.VehicleType)} can't be null or empty.");
                return result;
            }

            if (!request.BasePrice.HasValue)
            {
                result.SetError($"{nameof(request.BasePrice)} can't be null or empty.");
                return result;
            }

            if (!Enum.IsDefined(typeof(VehicleTypeEnum), request.VehicleType))
            {
                result.SetError($"{nameof(request.VehicleType)} value: {request.VehicleType} is not a valid VehicleTypeEnum.");
                return result;
            }

            if (request.BasePrice < 0)
            {
                result.SetError($"{nameof(request.BasePrice)} can't be lower than zero.");
                return result;
            }

            return result;
        }
    }
}
