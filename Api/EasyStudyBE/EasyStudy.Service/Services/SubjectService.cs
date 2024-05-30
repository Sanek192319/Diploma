using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Services;

namespace EasyStudy.Service.Services;

public class SubjectService : ISubjectService
{
    public async Task AddSubjectAsync(Subject newSubject, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSubjectAsync(Subject updatedSubject, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveSubject(int subjectId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Subject> GetSubjectAsync(int subjectId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Subject>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}