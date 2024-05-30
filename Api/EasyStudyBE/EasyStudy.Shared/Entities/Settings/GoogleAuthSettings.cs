using System.ComponentModel.DataAnnotations;

namespace EasyStudy.Shared.Entities.Settings;

/// <summary>
///     Google oauth settings
/// </summary>
public class GoogleAuthSettings
{
    /// <summary>
    ///     Gets or sets the google app identifier.
    /// </summary>
    [Required]
    public string AppId { get; set; }

    /// <summary>
    ///     Gets or sets the google app secret.
    /// </summary>
    [Required]
    public string AppSecret { get; set; }
}