using VectorAndPoint.ValTypes;

namespace OtusSpaceships.Interfaces
{
    public interface IVelocityChangable
    {
        int Direction { get; }
        int AmountOfDirections { get; }
        Vector Velocity { get; set; }
    }
}
