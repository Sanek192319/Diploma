using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Entities.Auth;

/// <summary>
///     Represents authorization result
/// </summary>
public class AuthResult
{
    /// <summary>
    /// Gets or sets auth access token
    /// </summary>
    public string Token { get; set; }

    /// <summary>
    /// Gets or sets auth refresh token
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Gets or sets auth error
    /// </summary>
    public string Error { get; set; }

    /// <summary>
    /// auth expired time
    /// </summary>
    public DateTimeOffset ValidTo { get; set; }

    /// <summary>
    /// Gets or sets auth user role
    /// </summary>
    public Role Role { get; set; }
}