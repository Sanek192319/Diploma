using EasyStudy.Shared.Entities;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Services;

public interface ISubjectService
{
    Task AddSubjectAsync(Subject newSubject, CancellationToken token);

    Task UpdateSubjectAsync(Subject updatedSubject, CancellationToken token);

    Task RemoveSubject(int subjectId, CancellationToken token);

    Task<Subject> GetSubjectAsync(int subjectId, CancellationToken token);

    Task<List<Subject>> GetAllAsync(CancellationToken token);
}