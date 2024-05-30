using EasyStudy.Shared.Entities;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Services;

public interface ITeacherService
{
    Task AddTeacherAsync(Teacher newTeacher, CancellationToken token);

    Task UpdateTeacherAsync(Teacher updatedTeacher, CancellationToken token);

    Task RemoveTeacher(int teacherId, CancellationToken token);

    Task<Teacher> GetTeacherAsync(int teacherId, CancellationToken token);

    Task<List<Teacher>> GetAllAsync(CancellationToken token);
}