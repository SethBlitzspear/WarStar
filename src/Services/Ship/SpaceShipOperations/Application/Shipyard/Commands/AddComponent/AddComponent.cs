using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Shipyard.Commands.AddComponent;

public record AddComponentCommand() : IRequest<Guid>
{
    public required ComponentDto ComponentDto { get; set; }
}

public class AddComponent(IShipYardService shipYardService, IMapper mapper) : IRequestHandler<AddComponentCommand, Guid>
{
    public async Task<Guid> Handle(AddComponentCommand request, CancellationToken cancellationToken)
    {
        var componentId =  await shipYardService.AddComponent(mapper.Map<Component>(request.ComponentDto));

        if (request.ComponentDto.TopComponentId.HasValue)
        {
            var componentToUpdate = await shipYardService.GetComponent(request.ComponentDto.TopComponentId.Value);
            componentToUpdate.BottomComponentId = componentId;
            await shipYardService.UpdateComponent(componentToUpdate);
        }
        if (request.ComponentDto.BottomComponentId.HasValue)
        {
            var componentToUpdate = await shipYardService.GetComponent(request.ComponentDto.BottomComponentId.Value);
            componentToUpdate.TopComponentId = componentId;
            await shipYardService.UpdateComponent(componentToUpdate);
        }
        if (request.ComponentDto.LeftComponentId.HasValue)
        {
            var componentToUpdate = await shipYardService.GetComponent(request.ComponentDto.LeftComponentId.Value);
            componentToUpdate.RightComponentId = componentId;
            await shipYardService.UpdateComponent(componentToUpdate);
        }
        if (request.ComponentDto.RightComponentId.HasValue)
        {
            var componentToUpdate = await shipYardService.GetComponent(request.ComponentDto.RightComponentId.Value);
            componentToUpdate.LeftComponentId = componentId;
            await shipYardService.UpdateComponent(componentToUpdate);
        }
        return componentId;
    }
}
