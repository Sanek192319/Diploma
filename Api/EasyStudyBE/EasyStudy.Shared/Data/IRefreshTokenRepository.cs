using EasyStudy.Shared.Entities.Auth;

namespace EasyStudy.Shared.Data;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken> GetByTokenAsync(string token, CancellationToken cToken);
}