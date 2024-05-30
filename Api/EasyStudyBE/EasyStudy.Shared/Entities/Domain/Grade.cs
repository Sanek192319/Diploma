namespace EasyStudy.Shared.Entities.Domain;

public class Grade : BaseEntity
{
    public uint Mark { get; set; }
    
    public Teacher GivenFrom { get; set; }
}