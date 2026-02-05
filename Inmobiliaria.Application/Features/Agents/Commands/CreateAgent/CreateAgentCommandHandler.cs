using Inmobiliaria.Application.Abstractions.Repositories;
using Inmobiliaria.Application.Utilities.Responses;
using Inmobiliaria.Domain.Entities;
using static Inmobiliaria.Application.Abstractions.Mediator.IRequestHandler;

namespace Inmobiliaria.Application.Features.Agents.Commands.CreateAgent;

public sealed class CreateAgentCommandHandler : IRequestHandler<CreateAgentCommand, Guid>
{
    private readonly IAgentRepository _repository;

    public CreateAgentCommandHandler(IAgentRepository repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResult<Guid>> Handle(CreateAgentCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var agent = Agent.Create(
                userId: command.UserId,
                fullName: command.FullName,
                email: command.Email,
                phone: command.Phone
            );

            await _repository.CreateAsync(agent);

            return ServiceResult<Guid>.Success(agent.Id, "Agente creado correctamente.");
        }
        catch (Exception ex)
        {
            return ServiceResult<Guid>.Fail("Error al crear el agente", ex.Message);
        }
    }
}
