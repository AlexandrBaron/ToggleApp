using Microsoft.EntityFrameworkCore;
using Toggle.DAL.DbCreators;
using Toggle.DAL.Entities;

namespace Toggle.DAL.Repositories
{
    public class PersonRepository : Repository<PersonEntity>
    {
        private readonly ToggleDbContext _dbContext;
        public PersonRepository(ToggleDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async override Task<PersonEntity?> Get(Guid Id)
        {
            return await _dbContext.Set<PersonEntity>()
                .Include(p => p.Projects)
                .Include(p => p.Activities)
                .SingleOrDefaultAsync(p => p.Id == Id);
        }
    }
}
