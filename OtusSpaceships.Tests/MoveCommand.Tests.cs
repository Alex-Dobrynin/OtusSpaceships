using System;

using FluentAssertions;

using Moq;

using OtusSpaceships.Commands;
using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

using Xunit;

namespace OtusSpaceships.Tests
{
    public class MoveCommandTests
    {
        private readonly Mock<IMovable> _movableMock = new Mock<IMovable>();

        public MoveCommandTests()
        {

        }

        [Fact]
        public void MoveExecute_ShouldThrowException_WhenGettingPosition()
        {
            _movableMock.SetupGet(m => m.Position).Throws<InvalidOperationException>();
            _movableMock.SetupGet(m => m.Speed).Returns(new Vector(5, 2));

            var movable = _movableMock.Object;

            var command = new MoveCommand(movable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MoveExecute_ShouldThrowException_WhenSettingPosition()
        {
            _movableMock.SetupSet(m => m.Position).Throws<InvalidOperationException>();
            _movableMock.SetupGet(m => m.Speed).Returns(new Vector(5, 2));

            var movable = _movableMock.Object;

            var command = new MoveCommand(movable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void MoveExecute_ShouldThrowException_WhenGettingSpeed()
        {
            _movableMock.SetupProperty(m => m.Position, new Point(12, 5));
            _movableMock.SetupGet(m => m.Speed).Throws<InvalidOperationException>();

            var movable = _movableMock.Object;

            var command = new MoveCommand(movable);

            var result = () => command.Execute();
            result.Should().Throw<InvalidOperationException>();
        }

        [Theory]
        [InlineData(new double[] { 12, 5 }, new double[] { -7, 3 }, new double[] { 5, 8 })]
        [InlineData(new double[] { 0, 0 }, new double[] { -7, 3 }, new double[] { -7, 3 })]
        [InlineData(new double[] { 12, 5 }, new double[] { 0, 0 }, new double[] { 12, 5 })]
        public void MoveExecute_ShouldApplyNewPosition(double[] position, double[] speed, double[] expected)
        {
            _movableMock.SetupProperty(m => m.Position, new Point(position[0], position[1]));
            _movableMock.SetupGet(m => m.Speed).Returns(new Vector(speed[0], speed[1]));

            var movable = _movableMock.Object;

            var command = new MoveCommand(movable);

            command.Execute();

            movable.Position.Should().Be(new Vector(expected[0], expected[1]));
        }
    }
}