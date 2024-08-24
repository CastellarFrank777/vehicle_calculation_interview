using vehicle_calculation.common.Enums;

namespace vehicle_calculation.logic.models.Vehicle
{
    public record VehicleCalculationServiceModel(decimal BasePrice, VehicleTypeEnum VehicleType, decimal StorageFee = 100)
    {
        public decimal BasicFee { get; set; }
        public decimal SpecialFee { get; set; }
        public decimal AssociationFee { get; set; }
        public decimal TotalCost => BasePrice + BasicFee + SpecialFee + AssociationFee + StorageFee;
    }
}
