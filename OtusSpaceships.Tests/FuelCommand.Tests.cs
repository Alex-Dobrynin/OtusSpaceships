using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Exceptions;
using OtusSpaceships.Interfaces;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class FuelCommandTests
    {
        private readonly Mock<IHasFuel> _hasFuelMock = new Mock<IHasFuel>();


        public FuelCommandTests()
        {

        }

        [Fact]
        public void CheckFuelExecute_ShouldThrowException_WhenBurningSpeedBiggerThanAmount()
        {
            _hasFuelMock.SetupGet(x => x.AmountOfFuel).Returns(1);
            _hasFuelMock.SetupGet(x => x.FuelBurningSpeed).Returns(2);
            var command = new CheckFuelCommand(_hasFuelMock.Object);

            var result = command.Execute;

            result.Should().Throw<NotEnoughFuelException>();
        }

        [Fact]
        public void CheckFuelExecute_ShouldExecuteSuccessful()
        {
            _hasFuelMock.SetupGet(x => x.AmountOfFuel).Returns(2);
            _hasFuelMock.SetupGet(x => x.FuelBurningSpeed).Returns(1);
            var command = new CheckFuelCommand(_hasFuelMock.Object);

            var result = command.Execute;

            result.Should().NotThrow();
        }

        [Theory]
        [InlineData(10,2,8)]
        [InlineData(11,1,10)]
        [InlineData(2,2,0)]
        [InlineData(2,0,2)]
        public void BurnFuelExecute_ShouldLowerLevelOfFuel(int startLevel, int burningSpeed, int endLevel)
        {
            _hasFuelMock.SetupProperty(x => x.AmountOfFuel, startLevel);
            _hasFuelMock.SetupGet(x => x.FuelBurningSpeed).Returns(burningSpeed);
            var command = new BurnFuelCommand(_hasFuelMock.Object);

            command.Execute();

            _hasFuelMock.Object.AmountOfFuel.Should().Be(endLevel);
        }
    }
}
