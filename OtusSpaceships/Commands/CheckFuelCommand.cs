using OtusSpaceships.Exceptions;
using OtusSpaceships.Interfaces;

namespace OtusSpaceships.Commands
{
    public class CheckFuelCommand : ICommand
    {
        private readonly IHasFuel _hasFuel;

        public CheckFuelCommand(IHasFuel hasFuel)
        {
            _hasFuel = hasFuel;
        }

        public void Execute()
        {
            if (_hasFuel.AmountOfFuel - _hasFuel.FuelBurningSpeed < 0) throw new NotEnoughFuelException();
        }
    }
}
