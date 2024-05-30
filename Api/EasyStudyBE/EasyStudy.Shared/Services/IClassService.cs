using EasyStudy.Shared.Entities;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Services;

public interface IClassService
{
    Task AddClassAsync(Class newClass, CancellationToken token);

    Task UpdateClassAsync(Class updatedClass, CancellationToken token);

    Task RemoveClass(int classId, CancellationToken token);

    Task<Class> GetClassAsync(int classId, CancellationToken token);

    Task<List<Class>> GetAllAsync(CancellationToken token);
}