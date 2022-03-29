using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

namespace OtusSpaceships.Commands
{
    public class CorrelateInstantaneousVelocityCommand : ICommand
    {
        private readonly IRotatable _rotatable;
        private readonly IMovable _movable;

        public CorrelateInstantaneousVelocityCommand(IRotatable rotatable, IMovable movable)
        {
            _rotatable = rotatable;
            _movable = movable;
        }

        public void Execute()
        {
            var newX = _movable.Velocity.Length * Math.Cos(2.0 * Math.PI * _rotatable.Direction / _rotatable.AmountOfDirections);
            var newY = _movable.Velocity.Length * Math.Sin(2.0 * Math.PI * _rotatable.Direction / _rotatable.AmountOfDirections);
            _movable.Velocity = new Vector(Math.Round(newX), Math.Round(newY));
        }
    }
}
