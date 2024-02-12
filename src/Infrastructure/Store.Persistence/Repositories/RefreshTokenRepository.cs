using Microsoft.Extensions.Logging;
using Store.Auth.Interfaces;
using Store.Domain.Entities;

namespace Store.Persistence.Repositories;

internal class RefreshTokenRepository : BaseRepository<RefreshToken, long, RefreshTokenRepository>,
    IRefreshTokenRepository
{
    public RefreshTokenRepository(StoreDbContext dbContext, ILogger<RefreshTokenRepository> logger) : base(dbContext,
        logger)
    {
    }
}