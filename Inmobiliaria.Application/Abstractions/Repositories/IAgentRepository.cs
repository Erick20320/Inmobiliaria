using Inmobiliaria.Domain.Entities;

namespace Inmobiliaria.Application.Abstractions.Repositories;

public interface IAgentRepository
{
    Task<Guid> CreateAsync(Agent agent);
    Task<Agent?> GetByIdAsync(Guid id);
    Task<IEnumerable<Agent>> GetAllAsync();
    Task UpdateAsync(Agent agent);
    Task DeleteAsync(Guid id);
}
