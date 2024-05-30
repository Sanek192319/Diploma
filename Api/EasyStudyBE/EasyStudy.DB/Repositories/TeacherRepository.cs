using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class TeacherRepository : BaseRepository<Teacher>, ITeacherRepository
{
    public TeacherRepository(EasyStudyDbContext context) : base(context)
    {
    }
}