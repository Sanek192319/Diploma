using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{
    public StudentRepository(EasyStudyDbContext context) : base(context)
    {
    }
}