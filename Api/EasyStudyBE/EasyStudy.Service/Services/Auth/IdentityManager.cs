using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Auth;
using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Entities.Settings;
using EasyStudy.Shared.Services;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Rise.Core.Managers;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace EasyStudy.Service.Services.Auth;

/// <inheritdoc />
public class IdentityManager : IIdentityManager
{
    private readonly IGoogleService _googleService;
    private readonly ILogger<IdentityManager> _logger;
    private readonly SecuritySettings _securitySettings;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///     Initializes a new instance of the <see cref="IdentityManager" /> class.
    /// </summary>
    /// <param name="securitySettings">The <see cref="SecuritySettings" /></param>
    /// <param name="unitOfWork">The <see cref="IUnitOfWork" /></param>
    /// <param name="logger">The <see cref="ILogger" /></param>
    /// <param name="googleService">The <see cref="IGoogleService" /></param>
    public IdentityManager(
        SecuritySettings securitySettings,
        IUnitOfWork unitOfWork,
        ILogger<IdentityManager> logger,
        IGoogleService googleService)
    {
        _securitySettings = securitySettings;
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securitySettings.TokenSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ClockSkew = TimeSpan.FromMinutes(0)
        };
        _unitOfWork = unitOfWork;
        _logger = logger;
        _googleService = googleService;
    }

    /// <inheritdoc />
    public async Task<AuthResult> RefreshTokenAsync(string token, string refreshToken)
    {
        var validatedToken = GetPrincipalFromToken(token);
        if (validatedToken == null)
        {
            return new AuthResult { Error = "Invalid Token " };
        }

        var userId = validatedToken.Claims.Single(x => x.Type == "guid").Value;
        var user = await _unitOfWork.UserRepository.GetAsync(int.Parse(userId), default);
        //_logger.LogDebug("User with id: {UserId} found", user.ExternalId);

        //_logger.LogInformation("User with id: {UserId} is active and found in contractors list", user.ExternalId);

        var expiryDateUnix = long.Parse(validatedToken.Claims
            .Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            .AddSeconds(expiryDateUnix);

        if (expiryDateTimeUtc > DateTime.UtcNow)
        {
            return new AuthResult { Error = "This token hasn't expired yet" };
        }

        var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
        var storedRefreshToken = await _unitOfWork.RefreshTokenRepository.GetByTokenAsync(refreshToken, default);
        if (storedRefreshToken == null)
        {
            return new AuthResult { Error = "This refresh token does not exist" };
        }

        _logger.LogDebug("Tokens are valid. Refresh token found in database");

        if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
        {
            return new AuthResult { Error = "This token has expired " };
        }

        if (storedRefreshToken.Invalidated)
        {
            return new AuthResult { Error = "This token has been invalidated " };
        }

        if (storedRefreshToken.Used)
        {
            return new AuthResult { Error = "This token has used" };
        }

        if (storedRefreshToken.JwtId != jti)
        {
            return new AuthResult { Error = "This refresh token does not match this JWT " };
        }

        _logger.LogDebug("Refresh token is valid");

        storedRefreshToken.Used = true;
        await _unitOfWork.RefreshTokenRepository.UpdateAsync(storedRefreshToken, default);

        _logger.LogDebug("Refresh token is marked as used");
        return await GenerateAuthResultForUserAsync(user);
    }

    /// <inheritdoc />
    public async Task<AuthResult> LoginWithGoogleAsync(GoogleAuthBody body)
    {
        _logger.LogInformation("Initiating google auth");
        var tokenData = await _googleService.GetAccessTokenAsync(body);

        if (tokenData is null)
        {
            return new AuthResult
            {
                Error = "Something went wrong"
            };
        }

        var data = DecodeGoogleToken(tokenData.IdToken);

        _logger.LogInformation("Google access token received and parsed");

        //var user = await _userManager.GetsOrCreateWithEmailAsync(data.Email);
        var user = await _unitOfWork.UserRepository.AddAsync(new User(), default);

        user = await _unitOfWork.UserRepository.UpdateAsync(user, default);
        _logger.LogDebug("User with id: {UserId} found or created. Avatar updated", user.Id);
        return await GenerateAuthResultForUserAsync(user);
    }

    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var principal = tokenHandler.ValidateToken(token, _tokenValidationParameters, out var validatedToken);
            return IsJwtWithValidSecurityAlgorithm(validatedToken) ? principal : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                   StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task<AuthResult> GenerateAuthResultForUserAsync(User newUser)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_securitySettings.TokenSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, newUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
                new Claim(ClaimTypes.Role, newUser.Role.ToString()),
                new Claim("id", newUser.Id.ToString())
            }),
            Expires = DateTime.UtcNow.Add(_securitySettings.TokenLifetime),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            JwtId = token.Id,
            UserId = newUser.Id.ToString(),
            ExpiryDate = DateTime.UtcNow.AddMonths(6)
        };
        await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken, default);

        /*_logger.LogDebug(
            "Tokens are generated, refresh token is added to database. User id: {UserId}. Token valid to: {ValidTo}. Role: {Role}",
            newUser.ExternalId, token.ValidTo, newUser.Role.ToString());*/
        return new AuthResult
        {
            Token = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken.Token,
            ValidTo = token.ValidTo,
            Role = newUser.Role
        };
    }

    private GoogleAccountData DecodeGoogleToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;

        if (tokenS is null)
        {
            return null;
        }

        var email = tokenS.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
        var firstName = tokenS.Claims.FirstOrDefault(x => x.Type == "given_name")?.Value;
        var lastName = tokenS.Claims.FirstOrDefault(x => x.Type == "family_name")?.Value;
        var name = tokenS.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
        var emailVerified = bool.Parse(tokenS.Claims.FirstOrDefault(x => x.Type == "email_verified")?.Value ?? "false");
        var avatar = tokenS.Claims.FirstOrDefault(x => x.Type == "picture")?.Value;
        var user = new GoogleAccountData
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            EmailVerified = emailVerified,
            Name = name,
            Avatar = avatar
        };
        return user;
    }
}