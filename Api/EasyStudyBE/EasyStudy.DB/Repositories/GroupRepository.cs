using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.DB.Repositories;

public class GroupRepository : BaseRepository<Group>, IRepository<Group>
{
    public GroupRepository(EasyStudyDbContext context) : base(context)
    {
    }
}