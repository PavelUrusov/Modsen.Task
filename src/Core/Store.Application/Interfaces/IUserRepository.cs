using Store.Domain.Entities;

namespace Store.Application.Interfaces;

public interface IUserRepository : IRepository<User, Guid>
{
}