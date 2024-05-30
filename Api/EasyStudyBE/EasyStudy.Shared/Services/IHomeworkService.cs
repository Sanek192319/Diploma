using EasyStudy.Shared.Entities;
using EasyStudy.Shared.Entities.Domain;

namespace EasyStudy.Shared.Services;

public interface IHomeworkService
{
    Task AddHomeworkAsync(Homework newHomework, CancellationToken token);

    Task UpdateHomeworkAsync(Homework updatedHomework, CancellationToken token);

    Task RemoveHomework(int homeworkId, CancellationToken token);

    Task<Homework> GetHomeworkAsync(int homeworkId, CancellationToken token);

    Task<List<Homework>> GetAllAsync(CancellationToken token);
}