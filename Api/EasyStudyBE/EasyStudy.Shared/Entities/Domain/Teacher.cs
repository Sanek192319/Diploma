namespace EasyStudy.Shared.Entities.Domain;

public class Teacher : User
{
    public List<Subject> Subjects { get; set; }
    
    public List<Group> Groups { get; set; }
}