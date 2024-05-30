namespace EasyStudy.Shared.Entities.Domain;

public class Subject : BaseEntity
{
    public string Name { get; set; }
    
    public List<Teacher> Teachers { get; set; }
}