using Newtonsoft.Json;

namespace EasyStudy.Shared.Entities.Auth;

public class GoogleTokenResponse
{
    /// <summary>
    ///     The access token.
    /// </summary>
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }

    /// <summary>
    ///     The expires time.
    /// </summary>
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    /// <summary>
    ///     The refresh token.
    /// </summary>
    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }

    /// <summary>
    ///     The scope.
    /// </summary>
    [JsonProperty("scope")]
    public string Scope { get; set; }

    /// <summary>
    ///     The token type.
    /// </summary>
    [JsonProperty("token_type")]
    public string TokenType { get; set; }

    /// <summary>
    ///     The id token.
    /// </summary>
    [JsonProperty("id_token")]
    public string IdToken { get; set; }

    /// <summary>
    ///     The error.
    /// </summary>
    [JsonProperty("error")]
    public string Error { get; set; }

    [JsonProperty("error_description")]
    public string ErrorDescription { get; set; }
}