namespace EasyStudy.Shared.Data;

//TODO: Add Transactions
public interface IUnitOfWork
{
    IGroupRepository GroupRepository { get; set; }
    
    IStudentRepository StudentRepository { get; set; }
    
    ITeacherRepository TeacherRepository { get; set; }
    
    IUserRepository UserRepository { get; set; }
    
    ISubjectRepository SubjectRepository { get; set; }
    
    IHomeworkRepository HomeworkRepository { get; set; }
    
    IGradeRepository Repository { get; set; }
    
    IRefreshTokenRepository RefreshTokenRepository { get; set; }
}