using Application.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Application.Users.Queries;
public sealed record GetUserByUserNameQuery(string Username) : IRequest<UserDto>;

public sealed class GetUserByUserName(IApplicationDbContext context, IMapper mapper) : IRequestHandler<GetUserByUserNameQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
    {
        var entity = await context.Users.FirstOrDefaultAsync(u => u.Username.Equals(request.Username), cancellationToken);
        return mapper.Map<UserDto>(entity);
    }
}