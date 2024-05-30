namespace EasyStudy.Shared.Entities.Domain;

public class User : BaseEntity
{
    public string FullName { get; set; }
    
    public string Email { get; set; }
    
    public Role Role { get; set; }
}