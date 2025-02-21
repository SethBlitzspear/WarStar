using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Components.Commands;
public sealed record UpdateComponentCommand : IRequest<ComponentDto>
{
    public required ComponentDto ComponentDto { get; set; }
    public Guid Id { get; set; }
}

public sealed class UpdateComponent(IApplicationDbContext context) : IRequestHandler<UpdateComponentCommand, ComponentDto>
{
    public async Task<ComponentDto> Handle(UpdateComponentCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Components.FindAsync(request.Id, cancellationToken);

        if (entity != null)
        {
            entity.SpaceShipId = request.ComponentDto.SpaceShipId;
            entity.ComponentTypeId = request.ComponentDto.ComponentTypeId;
            entity.Properties = request.ComponentDto.Properties;
            entity.TopComponentId = request.ComponentDto.TopComponentId;
            entity.BottomComponentId = request.ComponentDto.BottomComponentId;
            entity.LeftComponentId = request.ComponentDto.LeftComponentId;
            entity.RightComponentId = request.ComponentDto.RightComponentId;
            entity.Armour = request.ComponentDto.Armour;
            entity.StructuralIntegrity = request.ComponentDto.StructuralIntegrity;
            entity.LifeSupport = request.ComponentDto.LifeSupport;
            entity.MinPowerDraw = request.ComponentDto.MinPowerDraw;
            entity.MaxPowerDraw = request.ComponentDto.MaxPowerDraw;
            entity.Connections = (ConnectionType)request.ComponentDto.Connections;
            entity.PowerCouplings = (ConnectionType)request.ComponentDto.PowerCouplings;

            await context.SaveChangesAsync(cancellationToken);

            return new ComponentDto
            {
                Id = entity.Id,
                SpaceShipId = entity.SpaceShipId,
                TopComponentId = entity.TopComponentId,
                BottomComponentId = entity.BottomComponentId,
                LeftComponentId = entity.LeftComponentId,
                RightComponentId = entity.RightComponentId,
                Connections = (int)entity.Connections,
                PowerCouplings = (int)entity.PowerCouplings
            };
        }

        throw new EntityNotFoundException($"Unable to find Component with ID of {request.Id}");
    }
}