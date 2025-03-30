using Microsoft.EntityFrameworkCore;
using Toggle.DAL.DbCreators;
using Toggle.DAL.Entities;
namespace Toggle.DAL.Repositories
{
    public class Repository<T>
        where T : class, IEntity
    {
        private readonly ToggleDbContext _dbContext;
        public Repository(ToggleDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual IQueryable<T> Get()
        {
            return _dbContext.Set<T>();
        }
        public virtual async Task<T?> Get(Guid Id)
        {
            return await _dbContext.Set<T>().SingleOrDefaultAsync(t => t.Id == Id);
        }
        public async Task<bool> Delete(Guid Id)
        {
            if (await Get(Id) is not null)
            {
                var itemToRemove = await Get(Id);
                _dbContext.Remove(itemToRemove);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<Guid> Create(T entity)
        {
            if (await Get(entity.Id) is null)
            {
                _dbContext.Add(entity);
                _dbContext.SaveChanges();
                return entity.Id;
            }
            else
            {
                throw new ArgumentException("Item already exists");
            }
        }
        public async Task Update(T entity)
        {

            if (await Get(entity.Id) is not null)
            {
                //_dbContext.Entry(entity).State = EntityState.Detached;
                _dbContext.Attach(entity);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Item does not exists");
            }
        }
    }
}
