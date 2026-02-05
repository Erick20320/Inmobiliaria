namespace Inmobiliaria.Domain.Exceptions;

public class DomainRuleViolationException : Exception
{
    public string PropertyName { get; }

    public DomainRuleViolationException(string message)
        : base(message)
    {
        PropertyName = string.Empty;
    }

    public DomainRuleViolationException(string message, string propertyName)
        : base(message)
    {
        PropertyName = propertyName;
    }
}
