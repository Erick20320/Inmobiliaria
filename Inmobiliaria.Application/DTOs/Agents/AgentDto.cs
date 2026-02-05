namespace Inmobiliaria.Application.DTOs.Agents;

public record AgentDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public string? Phone { get; init; }
    public bool IsActive { get; init; }
}
