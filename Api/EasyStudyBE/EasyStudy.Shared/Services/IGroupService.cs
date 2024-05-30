using EasyStudy.Shared.Entities;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Services;

public interface IGroupService
{
    Task AddGroupAsync(Group newGroup, CancellationToken token);

    Task UpdateGroupAsync(Group updatedGroup, CancellationToken token);

    Task RemoveGroup(int groupId, CancellationToken token);

    Task<Group> GetGroupAsync(int groupId, CancellationToken token);

    Task<List<Group>> GetAllAsync(CancellationToken token);
}