using Inmobiliaria.Application.Features.Agents.DTOs;
using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Application.Features.Agents.Mappers;

public static class AgentMapperExtensions
{
    public static GetAgentAllQueryDto ToGetAgentAllDto(this Agent agent)
        => new()
        {
            Id = agent.Id,
            UserId = agent.UserId,
            FullName = agent.FullName,
            Email = agent.Email,
            Phone = agent.Phone,
            IsActive = agent.IsActive
        };

    public static GetAgentByIdQueryDto ToGetAgentByIdDto(this Agent agent)
        => new()
        {
            Id = agent.Id,
            UserId = agent.UserId,
            FullName = agent.FullName,
            Email = agent.Email,
            Phone = agent.Phone,
            IsActive = agent.IsActive
        };
}
