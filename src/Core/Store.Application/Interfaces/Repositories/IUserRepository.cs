using Store.Domain.Entities;

namespace Store.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{

}