using System.ComponentModel.DataAnnotations.Schema;

namespace EasyStudy.Shared.Entities.Auth;

[Table("refresh-tokens")]
public class RefreshToken : BaseEntity
{
    public string JwtId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Used { get; set; }
    public bool Invalidated { get; set; }
    public string UserId { get; set; }
}