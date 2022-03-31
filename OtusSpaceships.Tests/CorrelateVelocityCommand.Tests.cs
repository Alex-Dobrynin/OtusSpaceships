
using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class CorrelateVelocityCommandTests
    {
        private const int AmountOfDirections = 360;

        private readonly Mock<IVelocityChangable> _velocityChangableMock = new Mock<IVelocityChangable>();

        public CorrelateVelocityCommandTests()
        {

        }

        [Theory]
        [InlineData(5, 5, 135, -5, 5)]
        [InlineData(5, 5, 0, 7, 0)]
        [InlineData(5, 5, 45, 5, 5)]
        [InlineData(5, 5, 90, 0, 7)]
        [InlineData(5, 5, 225, -5, -5)]
        public void Execute_ShouldChangeVelocity(int startX, int startY, int direction, int endX, int endY)
        {
            _velocityChangableMock.SetupProperty(x => x.Velocity, new Vector(startX, startY));
            _velocityChangableMock.SetupGet(x => x.Direction).Returns(direction);
            _velocityChangableMock.SetupGet(x => x.AmountOfDirections).Returns(AmountOfDirections);
            var command = new CorrelateVelocityCommand(_velocityChangableMock.Object);

            command.Execute();

            _velocityChangableMock.Object.Velocity.Should().Be(new Vector(endX, endY));
        }
    }
}
