using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Agents.Commands.DeleteAgent;

public sealed class DeleteAgentCommandHandler : IRequestHandler<DeleteAgentCommand, Guid>
{
    private readonly IAgentRepository _repository;

    public DeleteAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Guid>> Handle(DeleteAgentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var agent = await _repository.GetByIdAsync(command.Id);

            if (agent is null)
                return ServiceResult<Guid>.Warning(command.Id, "Agente no encontrado.");

            await _repository.DeleteAsync(agent.Id);

            return ServiceResult<Guid>.Success(agent.Id, "Agente eliminado correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error al eliminar el agente", ex.Message);
        }
    }
}
