using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Services;

namespace EasyStudy.Service.Services;

public class GroupService : IGroupService
{
    public async Task AddGroupAsync(Group newGroup, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateGroupAsync(Group updatedGroup, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveGroup(int groupId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Group> GetGroupAsync(int groupId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Group>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}