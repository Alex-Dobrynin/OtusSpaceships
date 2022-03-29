using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Exceptions;
using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class MacroCommandTests
    {
        private const int AmountOfDirections = 360;

        private readonly Mock<IMovable> _movableMock = new Mock<IMovable>();
        private readonly Mock<IRotatable> _rotatableMock = new Mock<IRotatable>();
        private readonly Mock<IHasFuel> _hasFuelMock = new Mock<IHasFuel>();

        public MacroCommandTests()
        {

        }

        [Fact]
        public void MacroCommandExecute_ShouldExecuteEachCommand()
        {
            var command1 = new Mock<ICommand>();
            var command2 = new Mock<ICommand>();
            var command3 = new Mock<ICommand>();
            var macro = new MacroCommand(command1.Object, command2.Object, command3.Object);

            macro.Execute();

            command1.Verify(c => c.Execute(), Times.Once);
            command2.Verify(c => c.Execute(), Times.Once);
            command3.Verify(c => c.Execute(), Times.Once);
        }

        [Fact]
        public void MoveStraightForwardAlgo_ShouldThrowExceptionAndStop_WhenLackOfFuel()
        {
            _hasFuelMock.SetupGet(x => x.AmountOfFuel).Returns(1);
            _hasFuelMock.SetupGet(x => x.FuelBurningSpeed).Returns(2);
            var check = new CheckFuelCommand(_hasFuelMock.Object);
            var move = new Mock<ICommand>();
            var burn = new Mock<ICommand>();
            var macro = new MacroCommand(check, move.Object, burn.Object);

            var result = macro.Execute;

            result.Should().Throw<NotEnoughFuelException>();
            move.Verify(m => m.Execute(), Times.Never);
            burn.Verify(m => m.Execute(), Times.Never);
        }

        [Theory]
        [InlineData(0, 0, 5, 5, 10, 2, 5, 5, 8)]
        [InlineData(0, 0, 5, -5, 2, 2, 5, -5, 0)]
        public void MoveStraightForwardAlgo_ShouldMoveAndBurnFuel(int startX, int startY, int velocityX, int velocityY, int startFuel, int burnSpeed, int endX, int endY, int endFuel)
        {
            _hasFuelMock.SetupProperty(x => x.AmountOfFuel, startFuel);
            _hasFuelMock.SetupGet(x => x.FuelBurningSpeed).Returns(burnSpeed);
            _movableMock.SetupProperty(x => x.Position, new Point(startX, startY));
            _movableMock.SetupGet(x => x.Velocity).Returns(new Vector(velocityX, velocityY));

            var check = new CheckFuelCommand(_hasFuelMock.Object);
            var move = new MoveCommand(_movableMock.Object);
            var burn = new BurnFuelCommand(_hasFuelMock.Object);
            var macro = new MacroCommand(check, move, burn);

            macro.Execute();

            _hasFuelMock.Object.AmountOfFuel.Should().Be(endFuel);
            _movableMock.Object.Position.Should().Be(new Point(endX, endY));
        }

        [Theory]
        [InlineData(5, 5, 0, 45, 5, 5, 45)]
        [InlineData(5, 5, 45, 90, -5, 5, 135)]
        [InlineData(5, 5, 45, 45, 0, 7, 90)]
        [InlineData(5, 5, 45, -45, 7, 0, 0)]
        [InlineData(0, 7, 90, -45, 5, 5, 45)]
        public void RotateAlgo_ShouldRotateAndCorrelateVelocity(int startVelocityX, int startVelocityY, int startDirection, int angularVelocity, int endVelocityX, int endVelocityY, int endDirection)
        {
            _rotatableMock.SetupProperty(r => r.Direction, startDirection);
            _rotatableMock.SetupGet(r => r.AmountOfDirections).Returns(AmountOfDirections);
            _rotatableMock.SetupGet(r => r.AngularVelocity).Returns(angularVelocity);
            _movableMock.SetupProperty(x => x.Velocity, new Vector(startVelocityX, startVelocityY));

            var rotate = new RotateCommand(_rotatableMock.Object);
            var correlate = new CorrelateInstantaneousVelocityCommand(_rotatableMock.Object, _movableMock.Object);
            var macro = new MacroCommand(rotate, correlate);

            macro.Execute();

            _rotatableMock.Object.Direction.Should().Be(endDirection);
            _movableMock.Object.Velocity.Should().Be(new Vector(endVelocityX, endVelocityY));
        }
    }
}
