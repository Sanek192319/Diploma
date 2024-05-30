using Newtonsoft.Json;

namespace EasyStudy.Shared.Entities.Auth;

public class GoogleUserInfoResult
{
    [JsonProperty("issued_to")]
    public string IssuedTo { get; set; }

    [JsonProperty("audience")]
    public string Audience { get; set; }

    [JsonProperty("user_id")]
    public string UserId { get; set; }

    [JsonProperty("scope")]
    public string Scope { get; set; }

    [JsonProperty("expires_in")]
    public long ExpiresIn { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("verified_email")]
    public bool VerifiedEmail { get; set; }

    [JsonProperty("access_type")]
    public string AccessType { get; set; }
}