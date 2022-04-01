using OtusSpaceships.Interfaces;

namespace OtusSpaceships.Commands
{
    public class RotateCommand : ICommand
    {
        private readonly IRotatable _rotatable;

        public RotateCommand(IRotatable rotatable)
        {
            _rotatable = rotatable;
        }

        public void Execute()
        {
            var newDirection = (_rotatable.Direction + _rotatable.AngularVelocity) % _rotatable.AmountOfDirections;

            _rotatable.Direction = newDirection >= 0 ? newDirection : newDirection + _rotatable.AmountOfDirections;
        }
    }
}
