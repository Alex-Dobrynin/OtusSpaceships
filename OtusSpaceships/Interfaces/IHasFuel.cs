namespace OtusSpaceships.Interfaces
{
    public interface IHasFuel
    {
        public int FuelBurningSpeed { get; }
        public int AmountOfFuel { get; set; }
    }
}
