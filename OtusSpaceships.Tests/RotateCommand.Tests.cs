using System;

using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Interfaces;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class RotateCommandTests
    {
        private const int AmountOfDirections = 360;

        private readonly Mock<IRotatable> _movableMock = new Mock<IRotatable>();

        public RotateCommandTests()
        {

        }

        [Fact]
        public void RotateExecute_ShouldThrowException_WhenGettingDirection()
        {
            _movableMock.SetupGet(r => r.Direction).Throws<InvalidOperationException>();
            _movableMock.SetupGet(r => r.AmountOfDirections).Returns(AmountOfDirections);
            _movableMock.SetupGet(r => r.AngularVelocity).Returns(5);

            var rotatable = _movableMock.Object;

            var command = new RotateCommand(rotatable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void RotateExecute_ShouldThrowException_WhenSettingDirection()
        {
            _movableMock.SetupSet(r => r.Direction).Throws<InvalidOperationException>();
            _movableMock.SetupGet(r => r.AmountOfDirections).Returns(AmountOfDirections);
            _movableMock.SetupGet(r => r.AngularVelocity).Returns(5);

            var rotatable = _movableMock.Object;

            var command = new RotateCommand(rotatable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MoveExecute_ShouldThrowException_WhenGettingAngularVelocity()
        {
            _movableMock.SetupProperty(r => r.Direction, 15);
            _movableMock.SetupGet(r => r.AmountOfDirections).Returns(AmountOfDirections);
            _movableMock.SetupGet(r => r.AngularVelocity).Throws<InvalidOperationException>();

            var rotatable = _movableMock.Object;

            var command = new RotateCommand(rotatable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(15, 344, 359)]
        [InlineData(15, 345, 0)]
        [InlineData(0, 0, 0)]
        [InlineData(15, -360, 15)]
        [InlineData(15, -180, 195)]
        [InlineData(15, 360, 15)]
        public void MoveExecute_ShouldApplyNewPosition(int direction, int velocity, int expected)
        {
            _movableMock.SetupProperty(r => r.Direction, direction);
            _movableMock.SetupGet(r => r.AmountOfDirections).Returns(AmountOfDirections);
            _movableMock.SetupGet(r => r.AngularVelocity).Returns(velocity);

            var rotatable = _movableMock.Object;

            var command = new RotateCommand(rotatable);

            command.Execute();

            rotatable.Direction.Should().Be(expected);
        }
    }
}
