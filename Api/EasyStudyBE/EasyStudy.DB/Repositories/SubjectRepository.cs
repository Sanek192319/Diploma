using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
{
    public SubjectRepository(EasyStudyDbContext context) : base(context)
    {
    }
}