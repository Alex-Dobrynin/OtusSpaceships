using OtusSpaceships.Interfaces;

using VectorAndPoint.ValTypes;

namespace OtusSpaceships.Commands
{
    public class CorrelateVelocityCommand : ICommand
    {
        private readonly IVelocityChangable _velocityChangable;

        public CorrelateVelocityCommand(IVelocityChangable velocityChangable)
        {
            _velocityChangable = velocityChangable;
        }

        public void Execute()
        {
            var angle = 2.0 * Math.PI * _velocityChangable.Direction / _velocityChangable.AmountOfDirections;

            var newX = _velocityChangable.Velocity.Length * Math.Cos(angle);
            var newY = _velocityChangable.Velocity.Length * Math.Sin(angle);
            _velocityChangable.Velocity = new Vector(Math.Round(newX), Math.Round(newY));
        }
    }
}
