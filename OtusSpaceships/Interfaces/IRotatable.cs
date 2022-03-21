using OtusSpaceships.Models;

namespace OtusSpaceships.Interfaces
{
    public interface IRotatable
    {
        public int Direction { get; set; }
        public int AmountOfDirections { get; }
        public int AngularVelocity { get; }
    }
}
