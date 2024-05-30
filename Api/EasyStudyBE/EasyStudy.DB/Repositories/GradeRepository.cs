using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class GradeRepository : BaseRepository<Grade>, IGradeRepository
{
    public GradeRepository(EasyStudyDbContext context) : base(context)
    {
    }
    
}