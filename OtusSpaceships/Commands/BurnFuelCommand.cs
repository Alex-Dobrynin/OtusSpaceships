using OtusSpaceships.Interfaces;

namespace OtusSpaceships.Commands
{
    public class BurnFuelCommand : ICommand
    {
        private readonly IHasFuel _hasFuel;

        public BurnFuelCommand(IHasFuel hasFuel)
        {
            _hasFuel = hasFuel;
        }

        public void Execute()
        {
            _hasFuel.AmountOfFuel -= _hasFuel.FuelBurningSpeed;
        }
    }
}
