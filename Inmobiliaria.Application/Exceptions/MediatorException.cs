namespace Inmobiliaria.Application.Exceptions;

public sealed class MediatorException : Exception
{
    public MediatorException(string message)
        : base(message)
    {
    }
}
