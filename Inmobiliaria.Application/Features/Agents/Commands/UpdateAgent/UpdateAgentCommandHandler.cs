using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Exceptions;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Agents.Commands.UpdateAgent;

public sealed class UpdateAgentCommandHandler : IRequestHandler<UpdateAgentCommand, Guid>
{
    private readonly IAgentRepository _repository;

    public UpdateAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Guid>> Handle(UpdateAgentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var agent = await _repository.GetByIdAsync(command.Id);

            if (agent is null)
                return ServiceResult<Guid>.Warning(command.Id, "Agente no encontrado.");

            agent.SetFullName(command.FullName);
            agent.SetEmail(command.Email);
            agent.SetPhone(command.Phone);

            if (command.IsActive)
                agent.Activate();
            else
                agent.Deactivate();

            await _repository.UpdateAsync(agent);

            return ServiceResult<Guid>.Success(agent.Id, "Agente actualizado correctamente.");
        }
        catch (DomainRuleViolationException ex)
        {
            return ServiceResult<Guid>.Fail("Error de validación al actualizar el agente", ex.Message);
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error inesperado al actualizar el agente", ex.Message);
        }
    }
}
