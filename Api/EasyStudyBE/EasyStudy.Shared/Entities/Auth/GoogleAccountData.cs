namespace EasyStudy.Shared.Entities.Auth;

public class GoogleAccountData
{
    /// <summary>
    ///     Gets or sets first name.
    /// </summary>
    public string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets last name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    ///     Gets or sets email.
    /// </summary>
    public string Email { get; set; }

    public string Name { get; set; }

    public bool EmailVerified { get; set; }
    public string Avatar { get; set; }
}