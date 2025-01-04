namespace MySpot.Api.Exceptions
{
    public sealed class InvalidGuidException : CustomException
    {
        public InvalidGuidException() : base("Number guid is empty")
        {
        }
    }
}