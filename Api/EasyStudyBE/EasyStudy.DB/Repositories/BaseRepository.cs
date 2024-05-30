using EasyStudy.Shared.Data;
using EasyStudy.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace EasyStudy.DB.Repositories;

public class BaseRepository<T> : IRepository<T> where T: BaseEntity
{
    private readonly EasyStudyDbContext _context;

    private DbSet<T> DbSet => _context.Set<T>();

    public BaseRepository(EasyStudyDbContext context)
    {
        _context = context;
    }
    
    public async Task<T> AddAsync(T entity, CancellationToken token)
    {
        await DbSet.AddAsync(entity, token);
        return await DbSet.FindAsync(entity, token);
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken token)
    {
        DbSet.Update(entity);
        return await DbSet.FindAsync(entity, token);
    }

    public Task DeleteAsync(T entity, CancellationToken token)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task<T> GetAsync(int id, CancellationToken token)
    {
        return await DbSet.FindAsync(id, token);
    }

    public Task<List<T>> GetAllAsync(CancellationToken token)
    {
        return Task.FromResult(DbSet.ToList());
    }
}