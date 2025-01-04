namespace MySpot.Api.Exceptions
{
    public sealed class InvalidEmployeeNameException : CustomException
    {
        public string Name { get; }
        public InvalidEmployeeNameException(string name) 
            : base($"Employee name: {name} is invalide")
        {
            Name = name;
        }
    }
}