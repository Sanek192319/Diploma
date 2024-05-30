using EasyStudy.Shared.Entities.Auth;

namespace Rise.Core.Managers;

/// <summary>
///     A service for user authentication and authorization.
/// </summary>
public interface IIdentityManager
{
    /// <summary>
    ///     Refreshes access token for the user from refresh token
    /// </summary>
    /// <param name="token">Expired access token</param>
    /// <param name="refreshToken">The refresh token</param>
    /// <returns>Generated pair of access and refresh tokens</returns>
    Task<AuthResult> RefreshTokenAsync(string token, string refreshToken);

    /// <summary>
    ///     Logs in the user with the given code from google auth code flow
    /// </summary>
    /// <param name="body">The login request</param>
    /// <returns>Generated pair of access and refresh tokens</returns>
    Task<AuthResult> LoginWithGoogleAsync(GoogleAuthBody body);
}