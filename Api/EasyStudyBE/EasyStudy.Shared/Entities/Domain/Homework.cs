namespace EasyStudy.Shared.Entities.Domain;

public class Homework : BaseEntity
{
    public DateTime DateUploaded { get; set; }
    
    public string FileLink { get; set; }
    
    public Grade Grade { get; set; }
    
    public Student Student { get; set; }
}