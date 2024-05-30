using System.Net.Http.Headers;
using EasyStudy.Shared.Entities.Auth;
using EasyStudy.Shared.Entities.Settings;
using EasyStudy.Shared.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EasyStudy.Service.Services.Auth;

/// <inheritdoc />
public class GoogleService : IGoogleService
{
    private const string TokenValidationUrl =
        "https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={0}";

    private const string TokenRequestUrl = "https://oauth2.googleapis.com/token";
    private const string CalendarEventRequestUrl = "https://www.googleapis.com/calendar/v3/calendars/{0}/events";

    private const string UserInfoUrl =
        "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token={0}";

    private readonly GoogleAuthSettings _googleAuthSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<GoogleService> _logger;

    public GoogleService(GoogleAuthSettings googleAuthSettings, IHttpClientFactory httpClientFactory,
        ILogger<GoogleService> logger)
    {
        _googleAuthSettings = googleAuthSettings;
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<GoogleUserInfoResult> GetUserInfoAsync(string accessToken)
    {
        var formattedUrl = string
            .Format(UserInfoUrl, accessToken);
        var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
        result.EnsureSuccessStatusCode();
        var responseAsString = await result.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<GoogleUserInfoResult>(responseAsString);
    }

    /// <inheritdoc />
    public async Task<GoogleTokenValidationResult> ValidateTokenAsync(string token)
    {
        var formattedUrl = string.Format(TokenValidationUrl, token);
        var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);
        result.EnsureSuccessStatusCode();
        var responseAsString = await result.Content.ReadAsStringAsync();
        var toString = JsonConvert.DeserializeObject<GoogleTokenValidationResult>(responseAsString);
        return toString;
    }

    /// <inheritdoc />
    public async Task<GoogleTokenResponse> GetAccessTokenAsync(GoogleAuthBody body)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, TokenRequestUrl);

        var nvc = new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", _googleAuthSettings.AppId },
            { "client_secret", _googleAuthSettings.AppSecret },
            { "code", body.Code },
            { "access_type", "offline" },
            { "code_verifier", body.Verifier },
            { "redirect_uri", body.RedirectUrl }
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        request.Content = new FormUrlEncodedContent(nvc);

        _logger.LogDebug("Google: Trying to request token");
        var response = await _httpClientFactory.CreateClient().SendAsync(request);
        var stringData = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<GoogleTokenResponse>(stringData);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Google: Token retrieved successfully");
            return content;
        }

        _logger.LogWarning(
            "Google: Token request is unsuccessful with error: {ContentError}, description: {ContentErrorDescription}",
            content.Error, content.ErrorDescription);
        return null;
    }

    /// <inheritdoc />
    public async Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, TokenRequestUrl);

        var nvc = new Dictionary<string, string>
        {
            { "grant_type", "refresh_token" },
            { "client_id", _googleAuthSettings.AppId },
            { "client_secret", _googleAuthSettings.AppSecret },
            { "refresh_token", refreshToken }
        };

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        request.Content = new FormUrlEncodedContent(nvc);

        _logger.LogInformation("Google: Trying to refresh token");
        var response = await _httpClientFactory.CreateClient().SendAsync(request);
        var stringData = await response.Content.ReadAsStringAsync();
        var content = JsonConvert.DeserializeObject<GoogleTokenResponse>(stringData);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Google: Token refreshed successfully");
            return content;
        }

        _logger.LogWarning(
            "Google: Token refresh is unsuccessful with error: {ContentError}, description: {ContentErrorDescription}",
            content.Error, content.ErrorDescription);
        return null;
    }
}