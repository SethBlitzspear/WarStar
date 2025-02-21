using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Components.Commands;

public record CreateComponentCommand() : IRequest<Guid>
{
    public required ComponentDto ComponentData { get; set; }
}

public class CreateComponent(IApplicationDbContext context, IMapper mapper) : IRequestHandler<CreateComponentCommand, Guid>
{

    public async Task<Guid> Handle(CreateComponentCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<Component>(request.ComponentData);

        await context.Components.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
