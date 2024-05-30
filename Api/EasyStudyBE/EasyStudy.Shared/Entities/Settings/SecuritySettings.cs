using System.ComponentModel.DataAnnotations;

namespace EasyStudy.Shared.Entities.Settings;

/// <summary>
///     Security settings
/// </summary>
public class SecuritySettings
{
    /// <summary>
    ///     Gets or sets the secret key.
    /// </summary>
    [Required]
    public string TokenSecret { get; set; }

    /// <summary>
    ///     Gets or sets allowed hosts.
    /// </summary>
    public string AllowedOrigins { get; set; }

    /// <summary>
    ///     Gets or sets the token lifetim.
    /// </summary>
    [Required]
    public TimeSpan TokenLifetime { get; set; }

    /// <summary>
    ///     Gets or sets encryption key
    /// </summary>
    public string EncryptionKey { get; set; }
}