namespace EasyStudy.Shared.Entities.Auth;

/// <summary>
///     Represents google authorization body
/// </summary>
public class GoogleAuthBody
{
    /// <summary>
    ///     Gets or sets temporary code that the client will exchange for an access token
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    ///     Gets or sets temporary code verifier
    /// </summary>
    public string Verifier { get; set; }

    /// <summary>
    ///     Gets or sets URL to which will be used to redirect user after the authorization is complete
    /// </summary>
    public string RedirectUrl { get; set; }
}