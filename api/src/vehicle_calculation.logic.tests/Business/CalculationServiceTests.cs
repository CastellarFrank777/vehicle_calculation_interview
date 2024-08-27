using Moq;
using vehicle_calculation.common.Enums; 
using vehicle_calculation.common.ErrorHandling;
using vehicle_calculation.logic.Business;
using vehicle_calculation.logic.interfaces.Business;
using vehicle_calculation.logic.models.Vehicle;

namespace vehicle_calculation.logic.tests.Business
{
    [TestClass]
    public class CalculationServiceTests
    {
        private Mock<ILogManager> _mockLogger;
        private ICalculationService _service;

        [TestInitialize]
        public void Setup()
        {
            _mockLogger = new Mock<ILogManager>();
            _service = new CalculationService(_mockLogger.Object);
        }

        [TestMethod]
        public async Task CalculateAsync_CommonVehicle_CorrectFees()
        {
            // Arrange
            var model = new VehicleServiceModel(1000, VehicleTypeEnum.Common);

            // Act
            var result = await _service.CalculateAsync(model);

            // Assert
            Assert.IsFalse(result.IsError);
            Assert.AreEqual(50, result.Data!.BasicFee); // BasicFee should be clamped between 10 and 50
            Assert.AreEqual(20, result.Data.SpecialFee); // 2% of 1000
            Assert.AreEqual(10, result.Data.AssociationFee); // 1000 falls into the 1000 range
        }

        [TestMethod]
        public async Task CalculateAsync_LuxuryVehicle_CorrectFees()
        {
            // Arrange
            var model = new VehicleServiceModel(1500, VehicleTypeEnum.Luxury);

            // Act
            var result = await _service.CalculateAsync(model);

            // Assert
            Assert.IsFalse(result.IsError);
            Assert.AreEqual(150, result.Data!.BasicFee); // BasicFee should be clamped between 25 and 200
            Assert.AreEqual(60, result.Data.SpecialFee); // 4% of 1500
            Assert.AreEqual(15, result.Data.AssociationFee); // 1500 falls into the 3000 range
        }

        [TestMethod]
        public async Task CalculateAsync_BasicFeeBoundaryConditions_CorrectCalculation()
        {
            // Below minimum limit for common vehicles
            var model = new VehicleServiceModel(5, VehicleTypeEnum.Common);

            var result = await _service.CalculateAsync(model);

            Assert.IsFalse(result.IsError);
            Assert.AreEqual(10, result.Data!.BasicFee); // Should be clamped to the minimum 10
        }

        [TestMethod]
        public async Task CalculateAsync_InvalidVehicleType_Error()
        {
            // Invalid VehicleType
            var model = new VehicleServiceModel(1000, (VehicleTypeEnum)999);

            var result = await _service.CalculateAsync(model);

            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.Error.Contains("is not a valid VehicleTypeEnum"));
        }

        [TestMethod]
        public async Task CalculateAsync_InvalidBasePrice_Error()
        {
            // Invalid VehicleType
            var model = new VehicleServiceModel(-1, VehicleTypeEnum.Common);

            var result = await _service.CalculateAsync(model);

            Assert.IsTrue(result.IsError);
            Assert.IsTrue(result.Error.Contains("BasePrice can't be lower"));
        }
    }
}
