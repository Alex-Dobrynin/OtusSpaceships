using VectorAndPoint.ValTypes;

namespace OtusSpaceships.Interfaces
{
    public interface IMovable
    {
        public Point Position { get; set; }
        public Vector Speed { get; }
    }
}
