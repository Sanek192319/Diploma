using EasyStudy.Shared.Entities.Auth;

namespace EasyStudy.Shared.Services;

public interface IGoogleService
{
    /// <summary>
    ///     Validates the Google token.
    /// </summary>
    /// <param name="code">Google token that should be validated</param>
    /// <returns>Result of validation</returns>
    Task<GoogleTokenValidationResult> ValidateTokenAsync(string code);

    /// <summary>
    ///     Gets the Google user info.
    /// </summary>
    /// <param name="accessToken">Google JWT token</param>
    /// <returns>User info from google endpoint</returns>
    Task<GoogleUserInfoResult> GetUserInfoAsync(string accessToken);

    /// <summary>
    ///     Gets the Google JWT access token using the code from Authorization Code Flow.
    /// </summary>
    /// <param name="body">Google code flow request with code</param>
    /// <returns>Google access token</returns>
    Task<GoogleTokenResponse> GetAccessTokenAsync(GoogleAuthBody body);

    /// <summary>
    ///     Refreshes the Google JWT access token using the refresh token.
    /// </summary>
    /// <param name="refreshToken">Google refresh token</param>
    /// <returns></returns>
    Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken);
}