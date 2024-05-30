using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Services;

namespace EasyStudy.Service.Services;

public class TeacherService : ITeacherService
{
    public async Task AddTeacherAsync(Teacher newTeacher, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateTeacherAsync(Teacher updatedTeacher, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveTeacher(int teacherId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Teacher> GetTeacherAsync(int teacherId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Teacher>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}