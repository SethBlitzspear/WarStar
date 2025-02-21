using Domain.Entities;

namespace Application.Repositories;

public interface IUserRepository
{
    Task<User> GetUserByUsername(string username);
    Task<Guid> CreateUser(User user);
}