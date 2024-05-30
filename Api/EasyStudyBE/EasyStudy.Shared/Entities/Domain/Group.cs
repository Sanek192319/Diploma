namespace EasyStudy.Shared.Entities.Domain;

/// <summary>
/// Indicates group of students
/// </summary>
public class Group : BaseEntity
{
    public string GroupName { get; set; }
    
    public IEnumerable<Student> Students { get; set; }
}