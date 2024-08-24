namespace vehicle_calculation.api.models.Response
{
    public record VehicleCalculationResponse(
        decimal BasicFee,
        decimal SpecialFee,
        decimal AssociationFee,
        decimal TotalCost,
        decimal StorageFee = 100
    );
}
