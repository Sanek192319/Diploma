using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class HomeworkRepository : BaseRepository<Homework>, IHomeworkRepository
{
    public HomeworkRepository(EasyStudyDbContext context) : base(context)
    {
    }
}