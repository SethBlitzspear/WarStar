using System.Runtime.Serialization;

namespace Application.Common.Exceptions;
[Serializable]
public sealed class EntityNotFoundException : Exception
{
    public EntityNotFoundException() : base("The requested item was not found.")
    { }

    public EntityNotFoundException(string? message) : base(message)
    { }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    { }
}