namespace OtusSpaceships.Exceptions
{
    public class NotEnoughFuelException : SystemException
    {
        public NotEnoughFuelException() : base()
        {

        }

        public NotEnoughFuelException(string? message) : base(message)
        {
        }

        public NotEnoughFuelException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
