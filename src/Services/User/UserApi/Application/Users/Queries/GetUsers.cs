using Application.Common.Dtos;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;
public record GetUsersQuery() : IRequest<List<UserDto>>;

public class GetUsers(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUsersQuery, List<UserDto>>
{
    public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Users.ToListAsync(cancellationToken);
        return mapper.Map<List<UserDto>>(entity);
    }
}