using System.Transactions;
using EasyStudy.Shared.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasyStudy.Shared.Data;

public interface IRepository<T> where T : BaseEntity
{
    Task<T> AddAsync(T entity, CancellationToken token);

    Task<T> UpdateAsync(T entity, CancellationToken token);

    Task DeleteAsync(T entity, CancellationToken token);

    Task<T> GetAsync(int id, CancellationToken token);

    Task<List<T>> GetAllAsync(CancellationToken token);
}