using Microsoft.EntityFrameworkCore;
using Toggle.DAL.DbCreators;
using Toggle.DAL.Entities;

namespace Toggle.DAL.Repositories
{
    public class ProjectRepository : Repository<ProjectEntity>
    {
        private readonly ToggleDbContext _dbContext;
        public ProjectRepository(ToggleDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async override Task<ProjectEntity?> Get(Guid Id)
        {
            return await _dbContext.Set<ProjectEntity>()
                .Include(pr => pr.People)
                .Include(pr => pr.Activities)
                .SingleOrDefaultAsync(pr => pr.Id == Id);
        }
    }
}
