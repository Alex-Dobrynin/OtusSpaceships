
using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class CorrelateInstantaneousVelocityCommandTests
    {
        private const int AmountOfDirections = 360;

        private readonly Mock<IMovable> _movableMock = new Mock<IMovable>();
        private readonly Mock<IRotatable> _rotatableMock = new Mock<IRotatable>();

        public CorrelateInstantaneousVelocityCommandTests()
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
            _movableMock.SetupProperty(x => x.Velocity, new Vector(startX, startY));
            _rotatableMock.SetupGet(x => x.Direction).Returns(direction);
            _rotatableMock.SetupGet(x => x.AmountOfDirections).Returns(AmountOfDirections);
            var command = new CorrelateInstantaneousVelocityCommand(_rotatableMock.Object, _movableMock.Object);

            command.Execute();

            _movableMock.Object.Velocity.Should().Be(new Vector(endX, endY));
        }
    }
}
