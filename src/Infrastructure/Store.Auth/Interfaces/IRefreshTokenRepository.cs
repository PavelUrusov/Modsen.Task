using Store.Application.Interfaces.Repositories;
using Store.Domain.Entities;

namespace Store.Auth.Interfaces;

public interface IRefreshTokenRepository : IRepository<RefreshToken, long>
{

}