using EasyStudy.Shared.Entities.Domain;
using EasyStudy.Shared.Services;

namespace EasyStudy.Service.Services;

public class HomeworkService : IHomeworkService
{
    public async Task AddHomeworkAsync(Homework newHomework, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateHomeworkAsync(Homework updatedHomework, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveHomework(int homeworkId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<Homework> GetHomeworkAsync(int homeworkId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Homework>> GetAllAsync(CancellationToken token)
    {
        throw new NotImplementedException();
    }
}