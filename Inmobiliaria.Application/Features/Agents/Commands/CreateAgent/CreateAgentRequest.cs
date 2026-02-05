namespace Inmobiliaria.Application.Features.Agents.Commands.CreateAgent;

public record CreateAgentRequest(
    string FullName,
    string Email,
    string? Phone
)
{
    public CreateAgentCommand ToCommand(Guid userId) 
        => new()
        {
        UserId = userId,
        FullName = FullName,
        Email = Email,
        Phone = Phone
    };
}
