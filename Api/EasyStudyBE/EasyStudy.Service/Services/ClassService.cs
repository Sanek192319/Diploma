using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Services;

namespace EasyStudy.Service.Services;

public class ClassService : IClassService
{
    public async Task AddClassAsync(Class newClass, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateClassAsync(Class updatedClass, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveClass(int classId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Class> GetClassAsync(int classId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Class>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}