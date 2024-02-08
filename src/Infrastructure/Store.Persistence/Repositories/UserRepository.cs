using Microsoft.Extensions.Logging;
using Store.Application.Interfaces;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class UserRepository : BaseRepository<User, Guid, UserRepository>, IUserRepository
{
    public UserRepository(StoreDbContext dbContext, ILogger<UserRepository> logger) : base(dbContext, logger)
    {
    }
}