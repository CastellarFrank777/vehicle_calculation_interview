using System.Net;
using Microsoft.AspNetCore.Mvc;
using vehicle_calculation.api.models.Request;
using vehicle_calculation.api.models.Response;
using vehicle_calculation.common.Action;
using vehicle_calculation.common.Enums;
using vehicle_calculation.logic.interfaces.Business;
using vehicle_calculation.logic.models.Vehicle;
using vehicle_calculation.mappings.Extensions;

namespace vehicle_calculation.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController(
        ILogger<VehicleController> logger,
        ICalculationService calculationService) : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger = logger;
        private readonly ICalculationService _calculationService = calculationService;

        [HttpPost]
        [Route("calculate")]
        public async Task<ActionResult<VehicleCalculationResponse>> Calculate(VehicleCalculationRequest request, CancellationToken token = default)
        {
            if (!request.VehicleType.HasValue)
            {
                return BadRequest($"{nameof(request.VehicleType)} can't be null or empty.");
            }

            if (!request.BasePrice.HasValue)
            {
                return BadRequest($"{nameof(request.BasePrice)} can't be null or empty.");
            }

            if (!Enum.IsDefined(typeof(VehicleTypeEnum), request.VehicleType))
            {
                return BadRequest($"{nameof(request.VehicleType)} value: {request.VehicleType} is not a valid VehicleTypeEnum.");
            }

            if (request.BasePrice < 0)
            {
                return BadRequest($"{nameof(request.BasePrice)} can't be lower than zero.");
            }

            var serviceModel = request.ToServiceModel();
            DataResult<VehicleCalculationServiceModel> calculationResult;
            try
            {
                calculationResult = await _calculationService.CalculateAsync(serviceModel, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unexpected Error at performing Vehicle Calculations.");
                return StatusCode((int)HttpStatusCode.InternalServerError,
                    "Unexpected Error at performing vehicle calculation, please try again or contact an administrator.");
            }

            if (calculationResult.IsError)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Unexpected Server Error, please try again or contact an administrator.");
            }

            return Ok(calculationResult.Data);
        }
    }
}
