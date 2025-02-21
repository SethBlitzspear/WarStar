using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;

namespace Application.Users.Queries;
public sealed record GetUserQuery(Guid Id) : IRequest<UserDto>;

public sealed class GetUser(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Users.FindAsync(request.Id, cancellationToken);
        return mapper.Map<UserDto>(entity);
    }
}