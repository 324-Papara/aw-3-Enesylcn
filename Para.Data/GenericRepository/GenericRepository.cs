using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Para.Base.Entity;
using Para.Data.Context;
using Dapper;

namespace Para.Data.GenericRepository;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly ParaDbContext dbContext;

    public GenericRepository(ParaDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Save()
    {
        await dbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetById(long Id)
    {
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
    }

    public async Task Insert(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        await dbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        entity.IsActive = true;
        entity.InsertDate = DateTime.UtcNow;
        entity.InsertUser = "System";
        dbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task Delete(long Id)
    {
        var entity = await dbContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == Id);
        if(entity is not null)
            dbContext.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAll()
    {
       return await dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<List<TEntity>> Where(Expression<Func<TEntity, bool>> predicate)
    {
        return await dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> Include(params Expression<Func<TEntity, object>>[] includes)
    {
         var query = dbContext.Set<TEntity>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.ToListAsync();
    }
}