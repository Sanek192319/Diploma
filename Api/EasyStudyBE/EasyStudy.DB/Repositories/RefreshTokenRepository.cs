using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Auth;

namespace EasyStudy.DB.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(EasyStudyDbContext context) : base(context)
    {
    }

    public async Task<RefreshToken> GetByTokenAsync(string token, CancellationToken cToken)
    {
        throw new NotImplementedException();
    }
}